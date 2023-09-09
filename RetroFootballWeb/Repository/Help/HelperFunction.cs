using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using System.Data;

namespace RetroFootballWeb.Repository.Help
{
    public class HelperFunction
    {
        public HelperFunction()
        {
        }
        private static HelperFunction help;
        public static HelperFunction Help
        {
            get
            {
                if (help == null) help = new HelperFunction();
                return help;
            }
            set { help = value; }
        }
        public string AutoID(string type, string temp)
        {
            string ID = type + "001";      

            if (!string.IsNullOrEmpty(temp))
            {
                int num = int.Parse(temp.Substring(2)) + 1;
                temp = num.ToString();
                while (temp.Length < 3) temp = "0" + temp;
                ID = type + temp;
            }

            return ID;
        }
        public bool ValidProduct(Product product)
        {
            if (String.IsNullOrEmpty(product.ID)) return false;
            if (String.IsNullOrEmpty(product.Name)) return false;
            if (String.IsNullOrEmpty(product.Club)) return false;
            if (String.IsNullOrEmpty(product.Season)) return false;
            if (String.IsNullOrEmpty(product.Nation)) return false;
            if (String.IsNullOrEmpty(product.Image)) return false;
            if (String.IsNullOrEmpty(product.Status)) return false;
            if (product.Price <= 0 || product.Size_s < 0 || product.Size_m < 0 || product.Size_l < 0 || product.Size_xl < 0) return false;
            return true;
        }
    }
}
