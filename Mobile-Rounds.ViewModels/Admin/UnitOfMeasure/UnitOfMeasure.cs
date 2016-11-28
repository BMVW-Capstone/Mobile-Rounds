using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Admin.UnitOfMeasure
{
    public enum ModificationType
    {
        Create,
        Update,
        Delete
    };

    public class UnitOfMeasure : NotificationBase
    {
        private string abbreviation;
        private string fullName;
        private string modificationType;

        public Guid Id { get; set; }

        public string Abbreviation
        {
            get
            {
                return this.abbreviation;
            }

            set
            {
                this.abbreviation = value;
                this.RaisePropertyChanged(nameof(this.Abbreviation));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                this.fullName = value;
                this.RaisePropertyChanged(nameof(this.FullName));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        private AsyncCommand Save { get; set; }
        private AsyncCommand Cancel { get; set; }

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

        public void SetModificationType(ModificationType type)
        {
            const string New = "Save New Unit";
            const string Update = "Update Unit";
            const string Delete = "Delete Unit";

            switch (type)
            {
                case Admin.UnitOfMeasure.ModificationType.Create:
                    ModificationType = New;
                    break;
                case Admin.UnitOfMeasure.ModificationType.Update:
                    ModificationType = Update;
                    break;
                case Admin.UnitOfMeasure.ModificationType.Delete:
                    ModificationType = Delete;
                    break;
                default:
                    break;
            }
        }

        public UnitOfMeasure(AsyncCommand save, AsyncCommand cancel)
        {
            ModificationType = "Save New Unit";
            this.Save = save;
            this.Cancel = cancel;
        }

        public UnitOfMeasure(UnitOfMeasure toCopy)
        {
            this.fullName = toCopy.fullName;
            this.abbreviation = toCopy.abbreviation;
            this.Id = toCopy.Id;
            this.Save = toCopy.Save;
            this.Cancel = toCopy.Cancel;
            this.modificationType = toCopy.modificationType;
        }
    }
}
