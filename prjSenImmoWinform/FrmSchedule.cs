using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjSenImmoWinform.Models;
using prjSenImmoWinform.DAL;

namespace prjSenImmoWinform
{
    public partial class FrmScheduler : Form
    {
        private ClientRepository clientRep;

        public FrmScheduler()
        {
            InitializeComponent();
            clientRep = new ClientRepository();
        }

        private void timerSchedule_Tick(object sender, EventArgs e)
        {
            
            //Liberer les options arrivées à terme
            var lesOptions = clientRep.GetAllOptionsProspect().Where(o => o.DateFinOption <= DateTime.Now).ToList();
            foreach (var option in lesOptions)
            {
                ListViewItem lviOption = new ListViewItem(option.Lot.NumeroLot);
                lviOption.SubItems.Add(option.Client.NomComplet);
                lviOption.SubItems.Add(option.DatePriseOption.Value.ToShortDateString());
                lviOption.SubItems.Add(option.DateFinOption.Value.ToShortDateString());
                lvOptions.Items.Add(lviOption);
                //Lever l'option
                clientRep.LeverOption(option.Id);
                //Informer le commercial
            }
            //Identifier les activités échues non exécutées
            var lesActivitesCommerciales = clientRep.GetActivitesCommercialesEchues(DateTime.Now).ToList();
            lvActivitesCommerciales.Items.Clear();
            foreach (var ac in lesActivitesCommerciales)
            {
                if(ac.StatutActiviteCommerciale!= StatutActiviteCommerciale.Annulée && ac.StatutActiviteCommerciale != StatutActiviteCommerciale.Exécutée
                    && ac.StatutActiviteCommerciale != StatutActiviteCommerciale.EchueNonExecutée)
                {
                    clientRep.EchoirActiviteCommerciale(ac.Id);
                }
                ListViewItem lviAc = new ListViewItem(ac.DateActivite.ToShortDateString());
                lviAc.SubItems.Add(ac.HeureActivite.ToString().Substring(0, 5));
                lviAc.SubItems.Add(ac.Client.NomComplet);
                lviAc.SubItems.Add(ac.TypeActivite.ToString());
                lviAc.SubItems.Add(ac.Commentaire);
                //lviAc.SubItems.Add(ac.StatutActiviteCommerciale.ToString());

                if (ac.Priorite == Priorite.Faible)
                    lviAc.ImageIndex = 0;
                else
                  if (ac.Priorite == Priorite.Moyenne)
                    lviAc.ImageIndex = 1;
                else
                  if (ac.Priorite == Priorite.Haute)
                    lviAc.ImageIndex = 2;
                switch (ac.StatutActiviteCommerciale)
                {
                    case StatutActiviteCommerciale.NonEchue:
                        lviAc.BackColor = Color.White;
                        break;
                    case StatutActiviteCommerciale.Exécutée:
                        lviAc.BackColor = Color.LightGray;
                        break;
                    case StatutActiviteCommerciale.Renvoyée:
                        lviAc.BackColor = Color.Yellow;
                        break;
                    case StatutActiviteCommerciale.Annulée:
                        lviAc.BackColor = Color.Gainsboro;
                        break;
                    case StatutActiviteCommerciale.EchueNonExecutée:
                        lviAc.BackColor = Color.Salmon;
                        break;
                    default:
                        break;
                }
                lviAc.Tag = ac.Id;

                lvActivitesCommerciales.Items.Add(lviAc);
            }
        }
    }
}
