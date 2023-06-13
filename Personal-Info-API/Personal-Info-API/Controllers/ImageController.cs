using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Info_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        // GET: api/<ImageController>
        [HttpGet]
        public IActionResult GetPhotos()
        {
            // Retrieve all photos from the database
            List<Image> photos = new List<Image>();
            string connectionString = @"Data Source=DESKTOP-5NBKNN5;Initial Catalog=Details;Integrated Security=True;";
            string query = "SELECT * FROM Photos";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] photoData = (byte[])reader.GetValue(0);
                            string base64String = Convert.ToBase64String(photoData);
                            Image photo = new Image { ImageUpload = base64String };
                            photos.Add(photo);
                        }
                    }
                }
            }
            // Return the array of photo objects
            return Ok(photos);
        }


        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest();
            }

            string connectionString = @"Data Source=DESKTOP-5NBKNN5;Initial Catalog=Details;Integrated Security=True;";
            string query = "INSERT INTO Photos (ImageUpload) VALUES (@ImageUpload)";
            //Creates a byte array to store the file data
            byte[] ImageUpload;
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                ImageUpload = stream.ToArray();
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ImageUpload", SqlDbType.VarBinary, -1).Value = ImageUpload;
                        connection.Open();
                        command.ExecuteScalar();
                        connection.Close();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
