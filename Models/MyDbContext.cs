// using MySql.Data.MySqlClient;
// using OurInstagram.Models.Images;
// using OurInstagram.Models.Users;
//
// namespace OurInstagram.Models;
//
// public class MyDbContext
// {
//     private static string ConnectionString { get; set; }
//
//     public MyDbContext(string connectionString)
//     {
//         ConnectionString = connectionString;
//     }
//
//     private static MySqlConnection GetConnection()
//     {
//         return new MySqlConnection(ConnectionString);
//     }
//
//     public static User? GetCurrentUser(string username, string password)
//     {
//         List<User?> userList = new List<User?>();
//         using (MySqlConnection conn = GetConnection())
//         {
//             conn.Open();
//             MySqlCommand cmd = new MySqlCommand(
//                 $"select * from users where (username = '{username}' and password = '{password}')", conn
//             );
//             using (var reader = cmd.ExecuteReader())
//             {
//                 while (reader.Read())
//                 {
//                     userList.Add(new User()
//                     {
//                         userId = Convert.ToInt32(reader["userID"]),
//                         username = reader["username"].ToString(),
//                         password = reader["password"].ToString(),
//                         email = reader["email"].ToString(),
//                         phone = reader["phone"].ToString(),
//                         dateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(reader["dateOfBirth"])),
//                         gender = Convert.ToByte(reader["gender"]),
//                         avatarPath = reader["avatarPath"].ToString(),
//                         biography = reader["biography"].ToString(),
//                         displayedName = reader["displayName"].ToString(),
//                         images = GetImageList(Convert.ToInt32(reader["userID"]))
//                     });
//                 }
//             }
//         }
//
//         return userList.Count == 0 ? null : userList[0];
//     }
//
//     private static List<Image> GetImageList(int userId)
//     {
//         List<Image> imageList = new List<Image>();
//         using MySqlConnection conn = GetConnection();
//         conn.Open();
//         MySqlCommand cmd = new MySqlCommand(
//             $"select * from images where userID = {userId}", conn
//         );
//         using var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             imageList.Add(new Image()
//             {
//                 imageId = Convert.ToInt32(reader["imageID"]),
//                 imagePath = reader["imagePath"].ToString(),
//                 userId = userId,
//                 like = Convert.ToInt32(reader["like"])
//             });
//         }
//
//         return imageList;
//     }
//     public static void CreateNewUser(string username, string password)
//     {
//         using MySqlConnection conn = GetConnection();
//         conn.Open();
//         try
//         {
//             MySqlCommand cmd = new MySqlCommand(
//                 $"insert into users (username, password) values ('{username}', '{password}')", conn);
//             cmd.ExecuteNonQuery();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//         }
//     }
//
//
// }