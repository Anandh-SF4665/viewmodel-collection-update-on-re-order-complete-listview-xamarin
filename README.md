**[View document in Syncfusion Xamarin Knowledge base](https://www.syncfusion.com/kb/12220/how-to-update-viewmodel-collection-after-re-ordering-of-listview-items-are-complete-in)**

## Sample

```xaml
<StackLayout >
    <syncfusion:SfListView x:Name="ToDoListView" ItemSize="60" IsStickyHeader="True" ItemsSource="{Binding ToDoList}" DragStartMode="OnHold,OnDragIndicator" SelectionMode="None">

        <syncfusion:SfListView.DataSource>
            <data:DataSource>
                <data:DataSource.GroupDescriptors>
                    <data:GroupDescriptor PropertyName="CategoryName" />
                </data:DataSource.GroupDescriptors>
            </data:DataSource>
        </syncfusion:SfListView.DataSource>

        <syncfusion:SfListView.GroupHeaderTemplate>
            <DataTemplate>
                <code>
                . . .
                . . . 
                <code>
            </DataTemplate>
        </syncfusion:SfListView.GroupHeaderTemplate>

        <syncfusion:SfListView.ItemTemplate>
            <DataTemplate>
                <code>
                . . .
                . . .
                <code>
            </DataTemplate>
        </syncfusion:SfListView.ItemTemplate>
    </syncfusion:SfListView>
    <Button x:Name="saveButton" Text="Save" BackgroundColor="#949cdf" HeightRequest="50" Command="{Binding SaveOrderCommand}"/>
</StackLayout>

C#:
SaveButton.Clicked += SaveButton_Clicked;
ListView.ItemDragging += ListView_ItemDragging;

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
```
