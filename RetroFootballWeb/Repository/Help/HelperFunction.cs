using RetroFootballWeb.Models;

namespace RetroFootballWeb.Repository.Help
{
    public class HelperFunction
    {
        private readonly DataContext _context;
        public HelperFunction()
        {

        }
        public HelperFunction(DataContext context)
        {
            _context = context;
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
    }
}
