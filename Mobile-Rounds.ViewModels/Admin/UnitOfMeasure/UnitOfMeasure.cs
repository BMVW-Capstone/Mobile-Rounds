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
        private AsyncCommand parentSave;
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
            }
        }

        public AsyncCommand Save { get; private set; }

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

        public UnitOfMeasure()
        {
            ModificationType = "Save New Unit";
            this.Save = new AsyncCommand(this.SetId, this.ValidateInput);
        }

        public UnitOfMeasure(AsyncCommand parentSave)
        {
            this.Save = new AsyncCommand(this.SetId, this.ValidateInput);
            this.parentSave = parentSave;
        }

        public UnitOfMeasure(UnitOfMeasure toCopy)
        {
            this.fullName = toCopy.fullName;
            this.abbreviation = toCopy.abbreviation;
            this.Id = toCopy.Id;
            this.Save = toCopy.Save;
            this.modificationType = toCopy.modificationType;
        }

        private void SetId(object input)
        {
            if (this.Id == Guid.Empty)
            {
                this.Id = Guid.NewGuid();
            }

            if (this.parentSave != null)
            {
                this.parentSave.Execute(this);
            }
        }

        private bool ValidateInput(object input)
        {
            return !string.IsNullOrEmpty(this.Abbreviation)
                && !string.IsNullOrEmpty(this.FullName);
        }
    }
}
