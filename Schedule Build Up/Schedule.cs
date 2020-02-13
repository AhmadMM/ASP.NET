 public void GetDataTable()
        {
            try
            {
                int counterRows = 0;
                //-------------------------------Intialize-------------------------//
                DataTable dtDay = new DataTable();
                DataTable dtPeriod = new DataTable();
                DataTable dtSchedule = new DataTable();
                DataTable dtGrid = new DataTable();
                //-------------------------------EOIntialize-------------------------//

                //-------------------------------Fillingdt-------------------------// 
                dtPeriod = Global.RetrieveFromdB("Extra_getPeriodsOfSchedule");
                dtDay = Global.RetrieveFromdB("Extra_getDaysOfSchedule");
               // if (Session["Role"].ToString() == "Student" || Session["Role"].ToString() == "Parent")
               // {
                    dtSchedule = Global.RetrieveFromdB("Extra_getSchedule", "PersonID", Session["PersonID"].ToString());
               // }
                //else if (Session["Role"].ToString() == "Teacher")
              //{
                    dtSchedule = Global.RetrieveFromdB("Extra_getScheduleTeachers", "PersonID", Session["PersonID"].ToString());
              //}
                //-------------------------------EOFillingdt-------------------------//
                dtGrid.Columns.Add("Period", typeof(string));
                dtGrid.Columns["Period"].Caption = "Period";

                foreach (DataRow dtRow in dtDay.Rows)//Column Setup Days
                {
                    string fieldDay = dtRow["Day"].ToString();
                    dtGrid.Columns.Add(fieldDay, typeof(string));
                }
                foreach (DataRow dtRow in dtPeriod.Rows)//RowSetup Periods
                {
                    string fieldPeriod = dtRow["Period"].ToString();
                    dtGrid.Rows.Add();
                    dtGrid.Rows[counterRows]["Period"] = fieldPeriod;
                    counterRows++;
                }
                foreach (DataRow dtRow in dtSchedule.Rows)
                {
                    string fieldDay = dtRow["Day"].ToString();
                    int fieldPeriod = Convert.ToInt16(dtRow["Period"].ToString()) - 1;
                    string fieldCourse = dtRow["Course"].ToString();
                    dtGrid.Rows[fieldPeriod][fieldDay] = fieldCourse;
                }

                gvSchedule.DataSource = dtGrid;
                gvSchedule.DataBind();

                Boolean hasData = false;
                //dtGrid.Rows.Add("hi", "Janeway", "Captain", "hi", "Janeway", "Captain", "hi", "Janeway");

                for (int col = 0; col < dtGrid.Columns.Count; col++)
                {


                    for (int row = 0; row < dtGrid.Rows.Count; row++)
                    {
                        string val = dtGrid.Rows[row].ItemArray[col].ToString();

                        if (!String.IsNullOrEmpty(dtGrid.Rows[row].ItemArray[col].ToString()))
                        {


                            hasData = true;


                            break;


                        }else
                        {
                            gvSchedule.HeaderRow.Cells[col].Visible = false;
                            for (int hiddenrows = 0; hiddenrows < dtGrid.Rows.Count; hiddenrows++)
                            {
                                gvSchedule.Rows[hiddenrows].Cells[col].Visible = false;
                            }


                        }


                    }



                }
                hasData = false;
            }
            catch (Exception ex) { } 
        }
