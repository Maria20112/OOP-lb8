using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb2
{
    internal class Presenter
    {
        private readonly People _model;
        private readonly IPeopleView _view;

        public Presenter(IPeopleView view, People model)
        {
            _view = view;
            _model = model;

            _model.NotifyAdd += OnModelChanged;
            _model.NotifyRemove += OnModelChanged;
        }

        public void AddPerson(string person_name, string person_surname, string person_gender,
            string person_year_of_birth, string person_city, string person_country, string person_height)
        {
            Person newPerson;
            if (person_name == "")
            {
                newPerson = new Person();
            }
            else if (person_surname == "")
            {
                newPerson = new Person(person_name);
            }
            else if (person_city == "" || person_country == "" || person_height == "")
            {
                newPerson = new Person(person_name, person_surname);
            }
            else
            {
                newPerson = new Person(person_name, person_surname, person_gender, person_year_of_birth, person_city, person_country, person_height);
            }
            _model.Add(newPerson);
            // View обновится через событие NotifyAdd
        }

        public void DeletePerson(int index)
        {
            _model.Remove(index);
            // View обновится через событие NotifyRemove
        }

        private void OnModelChanged()
        {
            var peopleData = ConvertToDataTable(_model.getAll());
            _view.UpdatePeopleTable(peopleData);
        }

        public DataTable Show_all()
        {
            return ConvertToDataTable(_model.getAll());
        }

        private DataTable ConvertToDataTable(IEnumerable<Person> people)
        {
            DataTable dataTable = new DataTable();
            Array all = people.ToArray();
            dataTable.Columns.Add("№", typeof(int));
            dataTable.Columns.Add("Имя", typeof(string));
            dataTable.Columns.Add("Фамилия", typeof(string));
            dataTable.Columns.Add("Пол", typeof(string));
            dataTable.Columns.Add("Год рожд.", typeof(string));
            dataTable.Columns.Add("Город", typeof(string));
            dataTable.Columns.Add("Страна", typeof(string));
            dataTable.Columns.Add("Рост", typeof(string));
            int count = 0;
            foreach (Person curr in all)
            {
                dataTable.Rows.Add(count + 1, curr.name, curr.surname, curr.Gender, curr.Year_of_birth, curr.City, curr.Country,
                    curr.Height);
                count++;
            }
            return dataTable;
        }

        ///// <summary>
        ///// делегат (добавление объекта в модель)
        ///// </summary>
        //public delegate void PresenterPersonAdd();
        ///// <summary>
        ///// делегат (удаление элемента из модели)
        ///// </summary>
        //public delegate void PresenterPersonRemove();
        ///// <summary>
        ///// событие (добавление элемента в модель)
        ///// </summary>
        //public event PresenterPersonAdd? PresenterNotifyAdd;
        ///// <summary>
        ///// событие (удаление элемента из модели)
        ///// </summary>
        //public event PresenterPersonRemove? PresenterNotifyRemove;

        //void changeTable() => Show_all();
        ///// <summary>
        ///// Объект класса People, хранит всех созданных людей
        ///// </summary>
        //private People people = new People();

        ///// <summary>
        ///// Функция, формирующая таблицу содержащую информацию обо всех людях в коллекции
        ///// </summary>
        ///// <returns>таблица с информацией</returns>
        //public DataTable Show_all()
        //{
        //    DataTable dataTable = new DataTable();
        //    Array all = people.getAll().ToArray();
        //    dataTable.Columns.Add("№", typeof(int));
        //    dataTable.Columns.Add("Имя", typeof(string));
        //    dataTable.Columns.Add("Фамилия", typeof(string));
        //    dataTable.Columns.Add("Пол", typeof(string));
        //    dataTable.Columns.Add("Год рожд.", typeof(string));
        //    dataTable.Columns.Add("Город", typeof(string));
        //    dataTable.Columns.Add("Страна", typeof(string));
        //    dataTable.Columns.Add("Рост", typeof(string));
        //    int count = 0;
        //    foreach (Person curr in all)
        //    {
        //        dataTable.Rows.Add(count + 1, curr.name, curr.surname, curr.Gender, curr.Year_of_birth, curr.City, curr.Country,
        //            curr.Height);
        //        count++;
        //    }
        //    return dataTable;
        //}

        ///// <summary>
        ///// Конструктор
        ///// </summary>
        //public Presenter() { }

        ///// <summary>
        ///// Функция, создающая и добавляющая нового человека в коллекцию
        ///// </summary>
        ///// <param name="person_name">Имя</param>
        ///// <param name="person_surname">Фамилия</param>
        ///// <param name="person_gender">Пол</param>
        ///// <param name="person_year_of_birth">Год рождения</param>
        ///// <param name="person_city">Город</param>
        ///// <param name="person_country">Страна</param>
        ///// <param name="person_height">Рост</param>
        //public void Add(string person_name, string person_surname, string person_gender, 
        //    string person_year_of_birth, string person_city, string person_country, string person_height)
        //{
        //    people.NotifyAdd += changeTable;
        //    Person newPerson;
        //    if (person_name == "")
        //    {
        //        newPerson = new Person();
        //    }
        //    else if (person_surname == "")
        //    {
        //        newPerson = new Person(person_name);
        //    }
        //    else if (person_city == "" || person_country == "" || person_height == "")
        //    {
        //        newPerson = new Person(person_name, person_surname);
        //    }
        //    else
        //    {
        //        newPerson = new Person(person_name, person_surname, person_gender, person_year_of_birth, person_city, person_country, person_height);
        //    }
        //    people.Add(newPerson);
        //    PresenterNotifyAdd?.Invoke();
        //}

        ///// <summary>
        ///// Функция, удаляющая человека по номеру строки
        ///// </summary>
        ///// <param name="number">номер удаляемого человека</param>
        //public void Delete(int number)
        //{
        //    people.NotifyRemove += changeTable;
        //    people.Remove(number);
        //    PresenterNotifyRemove?.Invoke();
        //}
    }
}
