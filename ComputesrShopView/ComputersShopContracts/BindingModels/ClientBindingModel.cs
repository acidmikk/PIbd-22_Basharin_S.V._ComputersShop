﻿namespace ComputersShopContracts.BindingModels
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
