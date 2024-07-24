﻿using DataLayer.Data;

namespace DataLayer
{
    public interface IClientDao
    {
        ClientEntity Create(ClientEntity client);

        ClientEntity Delete(string clientFiscalCode);

        ClientEntity Get(string clientFiscalCode);

        IEnumerable<ClientEntity> GetAll();

        ClientEntity Update(string codiceFiscale, ClientEntity client);
    }
}
