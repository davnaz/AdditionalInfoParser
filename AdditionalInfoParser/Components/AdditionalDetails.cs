using AdditionalInfoParser.Components;
using AdditionalInfoParser.DataProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalInfoParser
{
    class AdditionalDetails
    {
        public long Id;
        public bool catsok;
        public bool dogsok;
        public bool furnished;
        public bool nosmoking;
        public bool wheelchairaccess;
        public bool apartment;
        public bool condo;
        public bool cottagecabin;
        public bool duplex;
        public bool flat;
        public bool house;
        public bool inlaw;
        public bool loft;
        public bool townhouse;
        public bool manufactured;
        public bool assistedliving;
        public bool land;
        public bool wdinunit;
        public bool wdhookups;
        public bool laundryinbldg;
        public bool laundryonsite;
        public bool nolaundryonsite;
        public bool carport;
        public bool attachedgarage;
        public bool detachedgarage;
        public bool offstreetparking;
        public bool streetparking;
        public bool valetparking;
        public bool noparking;


        public AdditionalDetails()
        {
            Id = -1;
            catsok = false;
            dogsok = false;
            furnished = false;
            nosmoking = false;
            wheelchairaccess = false;
            apartment = false;
            condo = false;
            cottagecabin = false;
            duplex = false;
            flat = false;
            house = false;
            inlaw = false;
            loft = false;
            townhouse = false;
            manufactured = false;
            assistedliving = false;
            land = false;
            wdinunit = false;
            wdhookups = false;
            laundryinbldg = false;
            laundryonsite = false;
            nolaundryonsite = false;
            carport = false;
            attachedgarage = false;
            detachedgarage = false;
            offstreetparking = false;
            streetparking = false;
            valetparking = false;
            noparking = false;
        }

        public AdditionalDetails(DataRow dataRow)
        {
            Id = Convert.ToInt64(dataRow[0]);
            string content = dataRow[1].ToString();
            catsok = content.Contains(Resources.catsok) ? true : false;
            dogsok = content.Contains(Resources.dogsok) ? true : false;
            furnished = content.Contains(Resources.furnished) ? true : false;
            nosmoking = content.Contains(Resources.nosmoking) ? true : false;
            wheelchairaccess = content.Contains(Resources.wheelchairaccess) ? true : false;
            apartment = content.Contains(Resources.apartment) ? true : false;
            condo = content.Contains(Resources.condo) ? true : false;
            cottagecabin = content.Contains(Resources.cottagecabin) ? true : false;
            duplex = content.Contains(Resources.duplex) ? true : false;
            flat = content.Contains(Resources.flat) ? true : false;
            house = content.Contains(Resources.house) ? true : false;
            inlaw = content.Contains(Resources.inlaw) ? true : false;
            loft = content.Contains(Resources.loft) ? true : false;
            townhouse = content.Contains(Resources.townhouse) ? true : false;
            manufactured = content.Contains(Resources.manufactured) ? true : false;
            assistedliving = content.Contains(Resources.assistedliving) ? true : false;
            land = content.Contains(Resources.land) ? true : false;
            wdinunit = content.Contains(Resources.wdinunit) ? true : false;
            wdhookups = content.Contains(Resources.wdhookups) ? true : false;
            laundryinbldg = content.Contains(Resources.laundryinbldg) ? true : false;
            nolaundryonsite = content.Contains(Resources.nolaundryonsite) ? true : false;
            laundryonsite = nolaundryonsite ? false : content.Contains(Resources.laundryonsite) ? true : false;
            carport = content.Contains(Resources.carport) ? true : false;
            attachedgarage = content.Contains(Resources.attachedgarage) ? true : false;
            detachedgarage = content.Contains(Resources.detachedgarage) ? true : false;
            offstreetparking = content.Contains(Resources.offstreetparking) ? true : false;
            streetparking = offstreetparking ? false: content.Contains(Resources.streetparking) ? true : false;
            valetparking = content.Contains(Resources.valetparking) ? true : false;
            noparking = content.Contains(Resources.noparking) ? true : false;
        }

        public void InsertInDb()
        {
            SqlCommand insertOfferCommand = DataProvider.Instance.CreateSQLCommandForInsertSP();
            insertOfferCommand.Connection = DataProvider.Instance.Connection;
            //insertOfferCommand.Connection.Open();
            insertOfferCommand.Parameters.Clear();
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.Id, Id);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.catsok, catsok);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.dogsok, dogsok);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.furnished, furnished);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.nosmoking, nosmoking);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.wheelchairaccess, wheelchairaccess);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.apartment, apartment);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.condo, condo);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.cottagecabin, cottagecabin);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.duplex, duplex);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.flat, flat);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.house, house);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.inlaw, inlaw);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.loft, loft);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.townhouse, townhouse);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.manufactured, manufactured);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.assistedliving, assistedliving);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.land, land);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.wdinunit, wdinunit);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.wdhookups, wdhookups);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.laundryinbldg, laundryinbldg);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.laundryonsite, laundryonsite);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.nolaundryonsite, nolaundryonsite);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.carport, carport);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.attachedgarage, attachedgarage);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.detachedgarage, detachedgarage);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.offstreetparking, offstreetparking);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.streetparking, streetparking);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.valetparking, valetparking);
            insertOfferCommand.Parameters.AddWithValue(Constants.DbCellNames.noparking, noparking);
            DataProvider.Instance.ExecureSP(insertOfferCommand);
        }
    }
}
