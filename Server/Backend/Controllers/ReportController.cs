using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Swashbuckle.Swagger.Annotations;

namespace Backend.Controllers
{
    /// <summary>
    /// Provides the means for getting reports.
    /// </summary>
    [RoutePrefix("api/reports")]
    public class ReportController : ApiController
    {
        private const string SwaggerName = "Reports";
        private readonly ReadingRepository readingsDatasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public ReportController(DatabaseContext database)
        {
            this.readingsDatasource = new ReadingRepository(database);
        }

        /// <summary>
        /// Gets a report of a given days rounds and readings.
        /// </summary>
        /// <param name="reportDate">The date to get a report for.</param>
        /// <returns>A list of rounds.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get(DateTime reportDate)
        {
            var report = await this.readingsDatasource.GetReportAsync(reportDate);
            return this.Ok(report);
        }
    }
}