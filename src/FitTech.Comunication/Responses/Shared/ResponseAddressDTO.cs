﻿namespace FitTech.Comunication.Responses.Shared
{
    public class ResponseAddressDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Number { get; set; }
    }
}
