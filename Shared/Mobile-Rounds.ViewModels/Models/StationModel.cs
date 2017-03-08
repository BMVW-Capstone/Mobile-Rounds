using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.ReadingInput;
using Mobile_Rounds.ViewModels.Regular.Region;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Models
{
    public class StationModel : NotificationBase
    {
        /// <summary>
        /// The Id of this particular station.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Id of the region this station belongs to.
        /// </summary>
        public Guid RegionId { get; set; }

        /// <summary>
        /// The name of the station.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates if this record is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// The count of nested items.
        /// </summary>
        public int ItemCount { get; set; }

        public string ItemCountString
        {
            get
            {
                if (ItemCount != 1)
                {
                    return $"{ItemCount} Items";
                }
                return $"{ItemCount} Item";
            }
        }

        /// <summary>
        /// The list of items in this given station. Obtained
        /// by calling the /api/station/id endpoint.
        /// </summary>
        public IEnumerable<ItemModel> Items { get; set; }

        public AsyncCommand Navigate { get; private set; }

        public AsyncCommand NavigateRoot { get; set; }

        public StationModel()
        {
            this.Items = new List<ItemModel>();
        }

        public StationModel(RegionModelSource region)
            : this()
        {
            this.NavigateRoot = region.NavigateRoot;
            this.Navigate = new AsyncCommand(async(obj) =>
            {                
                var file = Platform.ServiceResolver.Resolve<IFileHandler>();
                var reads = await file.GetFileAsync("items.json");
                var vm = new ReadingInputScreenViewModel(region, this, reads);
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationInput, vm);
            });
        }
    }
}
