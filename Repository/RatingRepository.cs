using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Repository;

public class RatingRepository:IRatingRepository
{
    public IConfiguration config { get; }
    private _326223617BookStoreContext bookStoreContext;
    public RatingRepository(_326223617BookStoreContext bookStoreContext, IConfiguration configuration)
    {
        this.bookStoreContext = bookStoreContext;
        config = configuration;

    }


    public async Task<Rating> AddRating(Rating rating)
    {
       

            string query = "INSERT INTO RATING(HOST, METHOD, PATH, REFERER,USER_AGENT, Record_Date)" +
                           "VALUES (@HOST, @METHOD, @PATH, @REFERER,@USER_AGENT, @Record_Date)";

            using (SqlConnection cn = new SqlConnection(config.GetConnectionString("myShop")))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
            cmd.Parameters.AddWithValue("@HOST", rating.Host);
            cmd.Parameters.AddWithValue("@METHOD", rating.Method);
            cmd.Parameters.AddWithValue("@PATH", rating.Path);
            cmd.Parameters.AddWithValue("@REFERER", rating.Referer);
            cmd.Parameters.AddWithValue("@USER_AGENT", rating.UserAgent);
            cmd.Parameters.AddWithValue("@Record_Date", rating.RecordDate);
            cn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                cn.Close();
                return rating;
            }
    }

    }



