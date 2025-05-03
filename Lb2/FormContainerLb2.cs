namespace Lb2
{
    public partial class FormContainerLb2 : Form
    {
        /// <summary>
        /// Объект класса Presenterр, обеспечивает взаимодействие между моделью и представлением
        /// </summary>
        Presenter presenter = new Presenter();

        void changeTable() => showAllButton_Click(null, null);
        /// <summary>
        /// Обработчик события, выводит данные всех людей
        /// </summary>
        public FormContainerLb2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выводит данные всех людей в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAllButton_Click(object sender, EventArgs e)
        {
            showTable.DataSource = null;
            showTable.Rows.Clear();
            showTable.Columns.Clear();
            showTable.Refresh();
            showTable.DataSource = presenter.Show_all();
        }

        /// <summary>
        /// Функция, срабатывающая при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Функция, добавляющая новый объект Person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.PresenterNotifyAdd += changeTable;
                create_err.Text = "";
                presenter.Add(name.Text, surname.Text, (man.Checked) ? "мужской" : "женский",
                    year_of_birth.Text, city.Text, country.Text, height.Text);
                create_err.Text = "Готово!";

                name.Text = "";
                surname.Text = "";
                year_of_birth.Text = "2000";
                city.Text = "";
                country.Text = "";
                height.Text = "";
            }
            catch (MyOverflowException ex)
            {
                Win32.MessageBox(0, ex.Message + "\n" + ex.TimeOfExeption.ToString(), "Перенаселение", 0);
            }
            catch (PersonArgumentExeption ex)
            {
                create_err.Text = ex.Message + "\n" + ex.TimeOfExeption.ToString();
            }
        }

        /// <summary>
        /// Функция для удаления объекта по номеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            presenter.PresenterNotifyRemove += changeTable;
            presenter.Delete((int)number.Value);
        }
        
    }
}
