using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{

    public partial class MainWindow : Window
    {
        public double lnm, res,nnum;
        public Selectedoperator? selectedoperator,fopt=null,secondoperator=null;
        public bool temp=false,check=false,sample=false;
        private simp simp;
        
        public MainWindow()
        {
            InitializeComponent();
            acbutton.Click += Acbutton_Click;
            negbutton.Click += Negbutton_Click;
            perbutton.Click += Perbutton_Click;
            equalbutton.Click += Equalbutton_Click;
            simp = new();
        }

        private void Equalbutton_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(reslabel.Content.ToString(), out nnum);
            switch(selectedoperator)
            {
                case Selectedoperator.sbtn:
                    res = simp.sum(lnm, nnum);
                    break;
                case Selectedoperator.sbbtn:
                    res = simp.sub(lnm, nnum);
                    break;
                case Selectedoperator.mbtn:
                    res = simp.mul(lnm, nnum);
                    break;
                case Selectedoperator.dbtn:
                    res = simp.div(lnm, nnum);
                    break;
            }
            reslabel.Content = res.ToString();
            lnm = res;
            check = false;
        }


        


        private void Perbutton_Click(object sender, RoutedEventArgs e)
        {
            if (reslabel.Content.ToString() == "0")
            {
                return;
            }
            else
            {
                double.TryParse(reslabel.Content.ToString(), out lnm);
                lnm /= 100;
                reslabel.Content = lnm.ToString();
            }
        }

        private void Negbutton_Click(object sender, RoutedEventArgs e)
        {
            if (reslabel.Content.ToString() == "0")
            {
                return;
            }
            else
            {
                double.TryParse(reslabel.Content.ToString(), out lnm);
                lnm *= -1;
                reslabel.Content = lnm.ToString();
            }
        }

        private void numbutton_Click(object sender, RoutedEventArgs e)
        {
            int val = 0;
            
            val = int.Parse((sender as Button).Content.ToString());
            
            if (reslabel.Content.ToString() == "0"||temp==true)
            {
                
                reslabel.Content = $"{val}";
                temp = false;
                
            }
            else
            {
                reslabel.Content = $"{reslabel.Content}{val}";
               
            }
            if(sample==true)
                toplabel.Content= $"{toplabel.Content}{val}";
            if (sample == false)
            {
                toplabel.Content = val.ToString();
                sample = true;
            }

        }

        private void operation_Click(object sender, RoutedEventArgs e)
        {
           
            if (check == false)
            {
                if (double.TryParse(reslabel.Content.ToString(), out lnm))
                {
                    reslabel.Content = (sender as Button).Content.ToString();
                    
                    temp = true;
                }
            }
            if (check == true)
            {
                if (double.TryParse(reslabel.Content.ToString(), out nnum))
                {
                    reslabel.Content = (sender as Button).Content.ToString();
                    
                    temp = true;
                }
            }

            toplabel.Content = $"{toplabel.Content}{reslabel.Content}";
            if (sender == sbtn)
                selectedoperator = Selectedoperator.sbtn;
            if (sender == sbbtn)
                selectedoperator = Selectedoperator.sbbtn;
            if (sender == dbtn)
                selectedoperator = Selectedoperator.dbtn;
            if (sender == mbtn)
                selectedoperator = Selectedoperator.mbtn;
            if (check == true)
            {
                operating();
                fopt = selectedoperator;
            }
            
            if (check == false)
            {
                fopt = selectedoperator;
                check = true;
            }


        }

        private void operating()
        {
            
            switch (fopt)
            {
                case Selectedoperator.sbtn:
                    res = simp.sum(lnm, nnum);
                    break;
                case Selectedoperator.sbbtn:
                    res = simp.sub(lnm, nnum);
                    break;
                case Selectedoperator.mbtn:
                    res = simp.mul(lnm, nnum);
                    break;
                case Selectedoperator.dbtn:
                    res = simp.div(lnm, nnum);
                    break;
            }
            lnm = res;
            check = false;
            
        }

        private void pointbutton_Click(object sender, RoutedEventArgs e)
        {
            if (reslabel.Content.ToString().Contains(".")){
                
            }
            else
            {
                reslabel.Content = $"{reslabel.Content}{"."}";
               
            }
        }

        private void Acbutton_Click(object sender, RoutedEventArgs e)
        {
            reslabel.Content = "0";
            check = false;
            sample = false;
        }

        public enum Selectedoperator
        {
            sbtn,
            sbbtn,
            mbtn,
            dbtn
        }
        
    }
    public class simp
    {
      
        public double sum(double n1,double n2)
        {
            var db = new OperationDb
            {
                Number1 = n1,
                Number2 = n2,
                Operator = "+",
                res = n1 + n2,
                CreatedOn = DateTime.Now
            };
            Insert2Db(db, "Additions");
            return n1 + n2;
        }
        public double sub(double n1, double n2)
        {
            var db = new OperationDb
            {
                Number1 = n1,
                Number2 = n2,
                Operator = "-",
                res = n1 - n2,
                CreatedOn = DateTime.Now
            };
            Insert2Db(db, "Subtractions");
            
            return n1 - n2;
        }
        public double div(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            else
            {
                var db = new OperationDb
                {
                    Number1 = n1,
                    Number2 = n2,
                    Operator = "/",
                    res = n1 / n2,
                    CreatedOn = DateTime.Now
                };
                Insert2Db(db, "Division");
                return n1 / n2;
            }
        }
        public double mul(double n1, double n2)
        {
            var db = new OperationDb
            {
                Number1 = n1,
                Number2 = n2,
                Operator = "*",
                res = n1 * n2,
                CreatedOn = DateTime.Now
            };
            Insert2Db(db, "Multiplication");
            return n1 *n2;
        }

        const string MongoDBConnectionString = "mongodb://localhost";

        public IMongoCollection<OperationDb> GetCollections(string collectionName)
        {
            var client = new MongoClient(MongoDBConnectionString);
            var database = client.GetDatabase("Calculator");
            return database.GetCollection<OperationDb>(collectionName);

        }

        private void Insert2Db(OperationDb operationDb, string collectionName)
        {
            var collections = GetCollections(collectionName);
            collections.InsertOne(operationDb);
        }
    }
}





