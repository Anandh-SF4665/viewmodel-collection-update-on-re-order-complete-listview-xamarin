using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields
        SfListView ListView;
        Button SaveButton;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("ToDoListView");
            SaveButton = bindable.FindByName<Button>("saveButton");
            SaveButton.Clicked += SaveButton_Clicked;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            SaveButton.Clicked -= SaveButton_Clicked;
            ListView = null;
            SaveButton = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Call back
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var newList = new List<ToDoItem>();
            foreach (var item in ListView.DataSource.DisplayItems)
            {
                newList.Add((ToDoItem)item);
            }
            (ListView.BindingContext as ViewModel).ToDoList = new ObservableCollection<ToDoItem>(newList);
            App.Current.MainPage.DisplayAlert("Items order has been updated", "", "Close");
        }
        #endregion
    }
}
