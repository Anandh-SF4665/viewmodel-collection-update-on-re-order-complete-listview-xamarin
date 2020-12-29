using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace ListViewXamarin
{
    public class ToDoListRepository
    {
        #region Constructor

        public ToDoListRepository()
        {

        }

        #endregion

        #region Methods

        internal ObservableCollection<ToDoItem> GetToDoList()
        {
            var todoList = new ObservableCollection<ToDoItem>();
            var random = new Random();

            for (int i = 0; i < 20; i++)
            {
                var todoItem = new ToDoItem()
                {
                    Name = toDoLists[i],
                };
                todoList.Add(todoItem);
            }
            return todoList;
        }

        string[] toDoLists = new string[]
        {
            "Reserve party venue",
            "Choose party attire",
            "Compile guest list",
            "Choose invitation",
            "Create wedding website",
            "Buy wedding ring",
            "Apply marriage license",
            "Hire photographer",
            "Buy wedding dress",
            "Refine guest list",
            "Send invitations",
            "Hire florist",
            "Shop for decorations",
            "Hire musicians",
            "Arrange catering",
            "Shop for groceries",
            "Book hotel for guest",
            "Plan honeymoon",
            "Book transportation",
            "Order wedding cake",
        };
        #endregion
    }
}
