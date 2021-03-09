using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TTMTool.Controllers;

namespace TTMTool
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        [HttpPost("/api/Login")]
        [Produces("application/json")]
        public IActionResult LoginUser(EmployeeLoginModel user)
        {
            EmployeeModel EM = null;

            try
            {
                using (var con = db.Gdb())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spValidateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", user.userName);
                    cmd.Parameters.AddWithValue("@Password", Convert.ToInt32(user.password));
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        EM = new EmployeeModel();
                        EM.id = Convert.ToInt32(rd["id"]);
                        EM.userName = Convert.ToString(rd["UserName"]);
                        EM.level = Convert.ToString(rd["level_"]);

                    }
                    con.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }
            if (EM == null)
            {
                return NotFound("User not found");
            }
            return Ok(EM);
        }
        [HttpPost("/api/createUser")]
        public IActionResult CreateUser(EmployeeModel user)
        {
            var pok = 0;
            try
            {
                using (var con = db.Gdb())
                {
                    SqlParameter outputIdParam = new SqlParameter("@id", SqlDbType.Int);
                    outputIdParam.Direction = ParameterDirection.Output;
                    SqlCommand cmd = new SqlCommand("dbo.spCreateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", user.userName);
                    cmd.Parameters.AddWithValue("@name", user.name);
                    cmd.Parameters.AddWithValue("@FM#", user.FM);
                    cmd.Parameters.AddWithValue("@password", Convert.ToInt32(user.password));
                    cmd.Parameters.AddWithValue("@entryDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@level", user.level);
                    cmd.Parameters.Add(outputIdParam);
                    con.Open();

                    pok = cmd.ExecuteNonQuery();
                    con.Close();

                    user.id = (int)outputIdParam.Value;

                    TimeSheetsController.CreateTimeSheet(user);
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (pok == 0)
            {
                return NotFound("User not found");
            }
            return Ok("User is inserted");
        }

        [HttpGet("/api/manageUser")]
        public IActionResult GetAllUsers()
        {
            List<EmployeeModel> users = null;
            EmployeeModel EM = null;
            try
            {
                using (var con = db.Gdb())
                {
                    users = new List<EmployeeModel>();
                    SqlCommand cmd = new SqlCommand("dbo.spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    ///TODO -Popravit DBNull exception
                    while (rd.Read())
                    {
                        EM = new EmployeeModel();
                        EM.id = Convert.ToInt32(rd["id"]);
                        Object Value = rd["personalNo"];
                        if (Value != DBNull.Value)
                        {
                            EM.personalNo = Convert.ToInt32(rd["personalNo"]);
                        }
                        else { EM.personalNo = 0; }
                        EM.userName = Convert.ToString(rd["userName"]);
                        Value = rd["PW"];
                        if (Value != DBNull.Value)
                        {
                            EM.PW = Convert.ToInt32(rd["PW"]);
                        }
                        EM.name = Convert.ToString(rd["name"]);
                        EM.FM = Convert.ToString(rd["FM#"]);
                        EM.level = Convert.ToString(rd["level_"]);
                        EM.entryDate = Convert.ToDateTime(rd["entryDate"]);
                        Value = rd["email"];
                        if (Value != DBNull.Value)
                        {
                            EM.email = Convert.ToString(rd["email"]);
                        }
                        else { EM.email = "0"; }
                        EM.password = Convert.ToString(rd["password"]);
                        users.Add(EM);
                    }

                    con.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }
            if (users == null)
            {
                return NotFound("User not found");
            }
            return Ok(users);
        }

        [HttpPost("/api/deleteUser/{id}")]
        [Produces("application/json")]
        public IActionResult DeleteUser(int id)
        {
            int ida = Convert.ToInt32(id);
            if (id != null)
            {
                using (var con = db.Gdb())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spDeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();


                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    con.Close();
                }
                return Ok("User is deleted");
            }
            return NotFound("User not found");
        }
        [HttpPost("/api/editUser")]
        public IActionResult EditUser(EmployeeModel model)
        {
            if (model != null)
            {
                using (var con = db.Gdb())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spEditUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.id);
                    cmd.Parameters.AddWithValue("@personalNO", model.personalNo);
                    cmd.Parameters.AddWithValue("@userName", model.userName);
                    cmd.Parameters.AddWithValue("@name", model.name);
                    cmd.Parameters.AddWithValue("@level", model.level);
                    cmd.Parameters.AddWithValue("@PW", model.PW);
                    cmd.Parameters.AddWithValue("@FM#", model.FM);
                    cmd.Parameters.AddWithValue("@email", model.email);
                    con.Open();


                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    con.Close();
                }
                return Ok("User is updated");
            }
            return NotFound("User not found");
        }
    }

}
