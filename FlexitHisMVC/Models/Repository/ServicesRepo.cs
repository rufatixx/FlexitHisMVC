﻿using System;
using FlexitHisMVC.Models;
using MySql.Data.MySqlClient;

namespace FlexitHisMVC.Data
{
    public class ServicesRepo
    {
        private readonly string ConnectionString;

        public ServicesRepo(string conString)
        {
            ConnectionString = conString;
        }
        public List<ServiceObj> GetServicesByHospital(int hospitalID)
        {
            List<ServiceObj> serviceList = new List<ServiceObj>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();
                using (MySqlCommand com = new MySqlCommand("SELECT * FROM services where hospitalID=@hospitalID;", connection))
                {
                    com.Parameters.AddWithValue("hospitalID", hospitalID);

                    MySqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {
                            ServiceObj service = new ServiceObj();

                            service.ID = Convert.ToInt32(reader["id"]);
                            service.hospitalID = Convert.ToInt32(reader["hospitalID"]);
                            service.depID = Convert.ToInt32(reader["departmentID"]);
                            service.name = reader["name"].ToString();
                            service.price = Convert.ToDouble(reader["price"]);

                            serviceList.Add(service);

                           


                        }



                    }

                }
                connection.Close();
            }
            return serviceList;
        }
    }
}

