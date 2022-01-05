using MVC_CollegeWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC_CollegeWebApplication.Controllers.API
{
    public class StudentsController : ApiController
    {
        readonly string connectionString = "Data Source=DR;Initial Catalog=CollegeDB;Integrated Security=True";
        // GET: api/Students
        public IHttpActionResult Get()
        {
            List<Student> listOfStudents = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM Students";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = command.ExecuteReader();

                    if (dataFromDB.HasRows)
                    {
                        while (dataFromDB.Read())
                        {
                            listOfStudents.Add(new Student(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetDateTime(3), dataFromDB.GetString(4)));
                        }
                        return Ok(new { listOfStudents });
                    }
                    return Ok(new { listOfStudents });
                }
            }
            catch (SqlException)
            {
                throw ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //GET: api/Students/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM Students
                                  WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dataFromDB = command.ExecuteReader();

                    if (dataFromDB.Read())
                    {
                        Student student = new Student(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetDateTime(3), dataFromDB.GetString(4));
                        return Ok(new { student });
                    }
                    
                    return Ok(new { Message = "Not Found The Specified Student" });
                }
            }
            catch (SqlException err)
            {
               return BadRequest(err.Message);
            }
            catch (Exception err)
            {

               return BadRequest(err.Message);
            }
        }

        // POST: api/Students
        public IHttpActionResult Post([FromBody]Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"INSERT INTO Students (firstName,lastName,birthday,email)
                                      VALUES('{student.firstName}','{student.lastName}','{student.birthday}','{student.email}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowEffected =  command.ExecuteNonQuery();
                    return Ok( new { RowEffected = rowEffected,Messege = "Student Added Successfully" });
                }
            }
            catch (SqlException err)
            {
                return BadRequest(err.Message);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }

        // PUT: api/Students/5
        public IHttpActionResult Put(int id, [FromBody]Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE Students 
                                      SET firstName = '{student.firstName}',lastName = '{student.lastName}',birthday = '{student.birthday}',email = '{student.email}'
                                      WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    return Ok(new {Messege = "Edit Successfully" });
                }
            }
            catch (SqlException err)
            {

                return BadRequest(err.Message);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }


        }

        // DELETE: api/Students/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"DELETE FROM Students
                                  WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    return Ok(new {Messege = "Deleted sucess"});
                }
            }
            catch (SqlException err)
            {
                return BadRequest(err.Message);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
        }
    }
}
