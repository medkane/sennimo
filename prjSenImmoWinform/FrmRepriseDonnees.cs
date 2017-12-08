using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjSenImmoWinform
{
    public partial class FrmRepriseDonnees : Form
    {
        public FrmRepriseDonnees()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source= MEDKANE-PC\\SQLSERVER;Initial Catalog=BDSenImmo;Integrated Security=false; User Id=sa;Password=JHzZ3LR4"))
            {

                using (SqlCommand Cmd = new SqlCommand(@"Select  
                                                        DIRECTIOn
                                                              ,[Agc/Dpt]

                                                         FROM ImportParc
  
                                                              WHERE [Agc/Dpt] IS NOT NULL
     
                                                         GROUP BY DIRECTIOn,
                                                        
                                                              [Agc/Dpt]
                                                                                     
                                                        ", con))
                {

                    using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            int rowCount = 1;
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr[0].ToString() != string.Empty)
                                {
                                    string CodeDirection = dr[0].ToString().ToUpper();
                                    //string codePole = dr[1].ToString().ToUpper();

                                    string CodeDepAg = dr[1].ToString().ToUpper();

                                    if (CodeDepAg != string.Empty && CodeDirection != string.Empty)
                                    {


                                        var direction = (from dir in db.Directions
                                                         where dir.CodeDirection == CodeDirection
                                                         select dir).FirstOrDefault();

                                        if (direction != null)
                                        {
                                            DepartementAgence dep = new DepartementAgence()
                                            {
                                                CodeDepartementAgence = CodeDepAg,
                                                NomDepartementAgence = CodeDepAg,
                                                DirectionId = direction.DirectionId,
                                                Direction = direction,

                                            };
                                            db.DepartementAgences.Add(dep);
                                        }
                                        else
                                        {
                                            MessageBox.Show(CodeDirection);
                                        }

                                    }


                                }


                                //rowCount += 1;
                                //for (int i = 1; i < dt.Columns.Count + 1; i++)
                                //{
                                //    // Add the header the first time through 
                                //    if (rowCount == 2)
                                //        oSheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
                                //    oSheet.Cells[rowCount, i] = dr[i - 1].ToString();
                                //}
                            }
                            db.SaveChanges();
                            MessageBox.Show("OK");
                        }
                    }
                }
            }
        }
    }
}
