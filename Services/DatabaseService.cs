using MySqlConnector;
using GorselOdev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GorselOdev.Services
{
    public class DatabaseService
    {
        // Emülatörden bilgisayardaki MySQL'e eriþmek için 10.0.2.2 kullanýyoruz.
        // Veritabaný: todo_db, Kullanýcý: root, Þifre: (boþ)
        private string connectionString = "Server=localhost;Port=3308;Database=todo_db;Uid=root;Pwd=;";

        // 1. Verileri Listeleme (SELECT)
        public async Task<List<TodoItem>> GetItemsAsync()
        {
            var list = new List<TodoItem>();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    var sql = "SELECT * FROM todoitems"; // Tablo adýndan emin ol!
                    var cmd = new MySqlCommand(sql, conn);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new TodoItem
                            {
                                Id = reader.GetInt32("Id"),
                                Baslik = reader.GetString("Baslik"),
                                // GetBoolean yerine mutlaka GetInt32 kullan!
                                Tamamlandi = reader.GetInt32("Tamamlandi"),
                                Detay = reader.IsDBNull(reader.GetOrdinal("Detay")) ? "" : reader.GetString("Detay"),
                                Tarih = reader.IsDBNull(reader.GetOrdinal("Tarih")) ? "" : reader.GetString("Tarih"),
                                Saat = reader.IsDBNull(reader.GetOrdinal("Saat")) ? "" : reader.GetString("Saat")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Burada hata alýyor musun kontrol et
                Console.WriteLine("Hata: " + ex.Message);
            }
            return list;
        }

        // 2. Yeni Görev Ekleme (INSERT)
        public async Task SaveItemAsync(TodoItem item)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            // SORGUNUN ÖDEVDEKÝ YENÝ SÜTUNLARI (Detay, Tarih, Saat) ÝÇERDÝÐÝNDEN EMÝN OL
            string query = "INSERT INTO TodoItems (Baslik, Detay, Tarih, Saat, Tamamlandi) VALUES (@b, @d, @t, @s, @tm)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@b", item.Baslik);
            command.Parameters.AddWithValue("@d", item.Detay);
            command.Parameters.AddWithValue("@t", item.Tarih);
            command.Parameters.AddWithValue("@s", item.Saat);
            command.Parameters.AddWithValue("@tm", item.Tamamlandi);

            await command.ExecuteNonQueryAsync();
        }

        // 3. Görev Silme (DELETE)
        public async Task DeleteItemAsync(int id)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new MySqlCommand("DELETE FROM TodoItems WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Silme Hatasý: " + ex.Message);
            }
        }
    }
}