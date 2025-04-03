using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentForm
{
    public partial class StudentPage_Individual : Form
    {
        private MySqlConnection connection;
        private int studentId;

        public StudentPage_Individual(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            string connectionString = "server=localhost;database=StudentInfoDB;user=root;password=12345;";
            connection = new MySqlConnection(connectionString);
        }

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            try
            {
                connection.Open();
                string query = "SELECT s.studentId, s.firstName, s.lastName, s.middleName, s.houseNo, s.brgyName, " +
                               "s.municipality, s.province, s.region, s.country, s.birthdate, s.age, s.studContactNo, " +
                               "s.emailAddress, s.guardianFirstName, s.guardianLastName, s.hobbies, s.nickname, c.courseName, " +
                               "y.yearLvl FROM StudentRecordTB s " +
                               "JOIN CourseTB c ON s.courseId = c.courseId " +
                               "JOIN YearTB y ON s.yearId = y.yearId WHERE s.studentId = @studentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentId", studentId);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    labelStudentId.Text = "Student ID: " + reader["studentId"].ToString();
                    labelFirstName.Text = "First Name: " + reader["firstName"].ToString();
                    labelLastName.Text = "Last Name: " + reader["lastName"].ToString();
                    labelMiddleName.Text = "Middle Name: " + reader["middleName"].ToString();
                    labelAddress.Text = "Address: " + reader["houseNo"].ToString() + " " + reader["brgyName"].ToString() + ", " +
                                        reader["municipality"].ToString() + ", " + reader["province"].ToString();
                    labelRegion.Text = "Region: " + reader["region"].ToString();
                    labelCountry.Text = "Country: " + reader["country"].ToString();
                    labelBirthdate.Text = "Birthdate: " + reader["birthdate"].ToString();
                    labelAge.Text = "Age: " + reader["age"].ToString();
                    labelContact.Text = "Contact Number: " + reader["studContactNo"].ToString();
                    labelEmail.Text = "Email: " + reader["emailAddress"].ToString();
                    labelGuardian.Text = "Guardian: " + reader["guardianFirstName"].ToString() + " " + reader["guardianLastName"].ToString();
                    labelHobbies.Text = "Hobbies: " + reader["hobbies"].ToString();
                    labelNickname.Text = "Nickname: " + reader["nickname"].ToString();
                    labelCourse.Text = "Course: " + reader["courseName"].ToString();
                    labelYearLevel.Text = "Year Level: " + reader["yearLvl"].ToString();
                }
                else
                {
                    MessageBox.Show("No student found with ID " + studentId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student details: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
