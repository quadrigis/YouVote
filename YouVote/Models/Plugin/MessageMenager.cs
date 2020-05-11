namespace YouVote.Models.Plugin
{
    public static class MessageMenager
    {
        public static string Crud(string obj, string crud, bool res = false)
        {
            var sucs = res ? Resources.crud.CrudRes.Success : Resources.crud.CrudRes.Failed;
            var msg = string.Format("{1}{2}{0}{3}", obj, crud, Resources.crud.CrudRes.Was, sucs);
            return msg;
        }

        public static string Login(bool res = true)
        {
            var sucs = res ? Resources.crud.CrudRes.Success : Resources.crud.CrudRes.Failed;
            var msg = string.Format("{0}{1}", sucs, Resources.crud.CrudRes.Login);
            return msg;
        }

        public static string Register(bool res = true)
        {
            var sucs = res ? Resources.crud.CrudRes.Success : Resources.crud.CrudRes.Failed;
            var msg = string.Format("{0} {1}", sucs, Resources.crud.CrudRes.Register);
            return msg;
        }
    }
}