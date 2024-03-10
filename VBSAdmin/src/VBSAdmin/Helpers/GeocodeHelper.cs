using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeocodeSharp.Google;
using VBSAdmin.Data.VBSAdminModels;


namespace VBSAdmin.Helpers
{
    public class GeocodeHelper
    {
        public static async Task<GetGeoCodeResponse> GetGeoCode(string address)
        {
            GetGeoCodeResponse response = new GetGeoCodeResponse();
            var client = new GeocodeClient("INSERT GOOGLE API KEY HERE");

            var resp = await client.GeocodeAddress(address);
            if (resp.Status == GeocodeStatus.Ok)
            {
                var firstResult = resp.Results.First();
                var location = firstResult.Geometry.Location;
                var lat = location.Latitude;
                var lng = location.Longitude;

                response.Lat = lat.ToString();
                response.Long = lng.ToString();

                return response;
            }

            return null;
        }


        public static async Task<GetGeoCodeResponse> GetGeoCode(Child child)
        {
            return await GetGeoCode(GetFullAddress(child));
        }


        public static string GetFullAddress(Child child)
        {
            var childAddress = child.Address1 + ", ";
            if (!string.IsNullOrWhiteSpace(child.Address2))
            {
                childAddress += child.Address2 + ", ";
            }
            childAddress += child.City + ", ";
            childAddress += child.State + ", ";
            childAddress += child.Zip;

            return childAddress;
        }
    }


    public class GetGeoCodeResponse
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
