//namespace OnlineMenu
//{
////    public class BackGroundTask
////    {
////        OMContext _dbContext;
////        public BackGroundTask(OMContext dbContext)
////        {
////            _dbContext = dbContext;
////        }
////       public void CheckUnacceptedOrders()
////        {
////            var unacceptedOrders = _dbContext.Orders.Where(o => o.Status == false && o.TenantId==1).ToList();
////            foreach (var order in unacceptedOrders)
////            {
////                if (order.DateTime < DateTime.Now.AddMinutes(-5))
////                {
////                    _dbContext.Orders.Remove(order);
////                }
////            }
////            _dbContext.SaveChanges();
////        }
////    }
////}
