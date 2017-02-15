using Backend.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Tests.Helpers
{
    public abstract class BaseTestClass
    {
        protected DatabaseContext Context { get; set; }
        private DbContextTransaction Transaction { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new DatabaseContext("TestDatabase");

            //wrap all tests in a global transaction.
            Transaction = Context.Database.BeginTransaction();
        }

        protected void ConfigureRequest(ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        protected async Task<TReturn> GetData<TReturn>(Task<IHttpActionResult> request)
            where TReturn: new()
        {
            var data = await GetResponse(request);
            TReturn result;
            data.TryGetContentValue(out result);
            return result;
        }

        protected TReturn GetData<TReturn>(IHttpActionResult request)
            where TReturn : new()
        {
            var data = GetResponse(request);
            TReturn result;
            data.TryGetContentValue(out result);
            return result;
        }

        protected async Task<HttpResponseMessage> GetResponse(Task<IHttpActionResult> request)
        {
            var response = await request;
            return await response.ExecuteAsync(new CancellationToken());
        }

        protected HttpResponseMessage GetResponse(IHttpActionResult request)
        {
            return request.ExecuteAsync(new CancellationToken()).Result;
        }

        [TestCleanup]
        public void Clean()
        {
            //clear the database by rolling back the transaction.
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;

            //dispose of native resources.
            Context.Dispose();
            Context = null;
        }
    }
}
