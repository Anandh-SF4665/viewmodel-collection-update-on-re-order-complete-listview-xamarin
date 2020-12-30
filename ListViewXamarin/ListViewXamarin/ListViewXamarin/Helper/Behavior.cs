using Syncfusion.DataSource.Extensions;
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
            ListView.ItemDragging += ListView_ItemDragging;
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

        #region Methods
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var newList = new List<ToDoItem>();
            foreach (var item in ListView.DataSource.DisplayItems)
            {
                if (!(item is GroupResult))
                    newList.Add((ToDoItem)item);
            }
            (ListView.BindingContext as ViewModel).ToDoList = new ObservableCollection<ToDoItem>(newList);
            App.Current.MainPage.DisplayAlert("Items order has been updated", "", "Close");
        }

        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Drop)
            {
                int dropIndex = e.NewIndex;
                var dropItem = ListView.DataSource.DisplayItems[dropIndex];
                var dropedGroup = GetGroup(dropItem, e);

                if ((e.ItemData as ToDoItem).CategoryName != dropedGroup.Key.ToString())
                {
                    (e.ItemData as ToDoItem).CategoryName = dropedGroup.Key.ToString();
                }
            }
        }

        public GroupResult GetGroup(object itemData, ItemDraggingEventArgs args)
        {
            GroupResult itemGroup = null;

            foreach (var item in this.ListView.DataSource.DisplayItems)
            {
                if (itemData is GroupResult)
                {
                    if (args.OldIndex > args.NewIndex && item == itemData) break;
                    if (item is GroupResult) itemGroup = item as GroupResult;
                    if (args.OldIndex < args.NewIndex && item == itemData) break;
                }
                else
                {
                    if (item == itemData) break;
                    if (item is GroupResult) itemGroup = item as GroupResult;
                }
            }
            return itemGroup;
        }
        #endregion
    }
}
