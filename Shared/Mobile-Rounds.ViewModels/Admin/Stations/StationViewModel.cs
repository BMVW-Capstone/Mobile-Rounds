using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Stations
{
    public class StationViewModel : NotificationBase
    {
        public AsyncCommand Save { get; private set; }
        public AsyncCommand Cancel { get; private set; }

        internal StationModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                RaisePropertyChanged(nameof(Id));
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(IsDeleted));
                RaisePropertyChanged(nameof(RegionId));
                this.Cancel.RaiseExecuteChanged();
                this.Save.RaiseExecuteChanged();
            }
        }

        public Guid Id
        {
            get
            {
                return this.model.Id;
            }
            set
            {
                this.model.Id = value;
            }
        }
        public string Name
        {
            get
            {
                return this.model.Name;
            }
            set
            {
                this.model.Name = value;
                this.RaisePropertyChanged(nameof(Name));
                this.Cancel.RaiseExecuteChanged();
                this.Save.RaiseExecuteChanged();
            }
        }
        public bool IsDeleted
        {
            get
            {
                return this.model.IsDeleted;
            }
            set
            {
                this.model.IsDeleted = value;
                this.RaisePropertyChanged(nameof(IsDeleted));
            }
        }
        public Guid RegionId
        {
            get
            {
                return this.model.RegionId;
            }
            set
            {
                this.model.RegionId = value;
                RaisePropertyChanged(nameof(RegionId));
                this.Cancel.RaiseExecuteChanged();
                this.Save.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Sets the modification type for the region.
        /// </summary>
        /// <param name="type">The type of modification taking place.</param>
        public void SetModificationType(ModificationType type)
        {
            const string New = "Create";
            const string Update = "Update";
            const string Delete = "Delete";

            switch (type)
            {
                case Shared.ModificationType.Create:
                    this.ModificationType = New;
                    break;
                case Shared.ModificationType.Update:
                    this.ModificationType = Update;
                    break;
                case Shared.ModificationType.Delete:
                    this.ModificationType = Delete;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the type of modification that is currently taking place
        /// on the region
        /// </summary>
        public string ModificationType
        {
            get
            {
                return this.modificationType;
            }

            set
            {
                this.modificationType = value;
                this.RaisePropertyChanged(nameof(this.ModificationType));
            }
        }

        public StationViewModel(AsyncCommand save, AsyncCommand cancel)
        {
            this.model = new StationModel();
            this.Save = save;
            this.Cancel = cancel;
            this.SetModificationType(Shared.ModificationType.Create);
        }
        public StationViewModel(StationModel model, AsyncCommand save, AsyncCommand cancel)
            : this(save, cancel)
        {
            this.model = model;
            this.SetModificationType(Shared.ModificationType.Update);
        }

        private StationModel model;
        private string modificationType;
    }
}
