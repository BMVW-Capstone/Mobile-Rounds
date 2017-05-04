
Function GetTodaysReadings([string] $apiHost) {

    # Get todays date
    $todaysDate = Get-Date -Format "MM-dd-yyyy"

    # Build up the query for todays date.
    $query = "/api/reports?reportDate=" + "3-26-2017"

    # Build up the API request url
    $url = New-Object System.Uri -ArgumentList @([System.Uri]$apiHost, $query)

    # Build the actual web request
    $response = Invoke-WebRequest -Uri $url

    # Return the status code and the content so the user can react.
    return $response.StatusCode, ($response.Content | ConvertFrom-Json)
}

Function BuildOutOfSpecBody([object]$json) {
    $outOfSpecBody = 'The following readings were found to be out of spec:'

    Foreach($reading in $json.OutOfSpecReadings) {

        $region = $reading.Region
        $station=  $reading.Station
        $item = $reading.Item
        $meter = $reading.ItemMeter

        $recordedBy = $reading.Round.AssignedTo
            
        $recordedAt = ([DateTime]($reading.Reading.TimeTaken)).ToString("HH:mm")
        $recordedValue = $reading.Reading.Value
        $unit = $reading.UnitAbbreviation
        $comments = $reading.Reading.Comments

        if ([String]::IsNullOrEmpty($comments)){
            $comments = ""
        }

        $item1 = [string]::Format("<h4>Location: {0} -> {1} -> {2} -> {3}</h4>",
            $region, $station, $item, $meter)

        $item2 = [string]::Format("<li>Input Recorded By: {0}.</li>", $recordedBy)
        $item3 = [string]::Format("<li>Time Recorded: {0}.</li>", $recordedAt)
        $item4 = [string]::Format("<li>Recorded Value: {0} {1}.</li>", $recordedValue, $unit)
        $item5 = [string]::Format("<li>Comments: {0}</li>", $comments)

        $formatted = [string]::Format(“{0}<ul>{1}{2}{3}{4}</ul>”, $item1, $item2, $item3, $item4, $item5)
        $outOfSpecBody += $formatted
    }
    return $outOfSpecBody
}

Function ProcessReadings([Array] $response,
    [String] $smtp_host,
    [String] $smtp_user, 
    [String] $smtp_pass,
    [int] $smtp_port,
    [String]$recips) {
    
    if ($response[0] -ne 200) {
        "Failed to get todays report: ", $response[1]
    }
    else {
        $json = $response[1]

        #Build email credentials
        $credentials = New-Object system.Net.NetworkCredential
        $credentials.UserName = $smtp_user
        $credentials.Password = $smtp_pass

        # Send the email
        $smtp_client = New-Object system.Net.Mail.SmtpClient
        $smtp_client.Host = $smtp_host
        $smtp_client.Port = $smtp_port
        $smtp_client.EnableSsl = $true
        $smtp_client.Credentials = $credentials

        $missedRoundsBody = 'The following round hours were not completed by everyone: <span style="color:red">' `
            + [String]::Join(", ", $json.HoursMissed) + "</span><br>"


        $outOfSpecBody = BuildOutOfSpecBody($json)

        $subject = "Today's Summary of Rounds [" + (Get-Date -Format d) + "]"
        $sender = "mobile-rounds.automation@solarworld.com"

        $message = New-Object System.Net.Mail.MailMessage $sender, $recips
        $message.Subject = $subject
        $message.IsBodyHTML = $true
        $message.Body = $missedRoundsBody + $outOfSpecBody

        $smtp_client.send($message)
    }
}



Function Run([String] $apiHost,
    [String] $smtp_host,
    [String] $smtp_user, 
    [String] $smtp_pass,
    [int] $smtp_port,
    [string] $recips) {
    # Change this URL to change the API host.
    $apiResponse = GetTodaysReadings -apiHost $apiHost
    ProcessReadings -response $apiResponse `
        -smtp_host $smtp_host `
        -smtp_user $smtp_user `
        -smtp_pass $smtp_pass `
        -smtp_port $smtp_port `
        -recips $recips

}

Run -apiHost "https://localhost:44363" `
    -smtp_host "smtp.gmail.com" `
    -smtp_user "tyler.j.vanderhoef@gmail.com" `
    -smtp_pass "" `
    -smtp_port 587 `
    -recips "tyler.j.vanderhoef@gmail.com"