﻿namespace EadFacil.Core.Data.EventSourcing;

public class StoredEvent
{
   public Guid Id { get; private set; }
   public string Tipo { get; private set; }
   public DateTime DataOcorrencia { get; set; }
   public string Dados { get; private set; }
   
   public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
   {
      Id = id;
      Tipo = tipo;
      DataOcorrencia = dataOcorrencia;
      Dados = dados;
   }
}