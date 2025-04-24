using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using Spring2025_Samples.Models;


namespace Api.eCommerce.Database
{
    public class FirebaseService
    {
        public FirestoreDb db;

        public FirebaseService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("./Keys/application_default_credentials.json"),
                    ProjectId = "project-1267165130420421013"
                });
            }

            db = FirestoreDb.Create("project-1267165130420421013");
        }

        public async Task<List<Item?>> GetAllProducts()
        {
            QuerySnapshot querySnapshot = await db.Collection("products").GetSnapshotAsync();
            List<Item?> allProducts = new List<Item?>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Item? product = documentSnapshot.ConvertTo<Item?>();
                    allProducts.Add(product);
                }
            }
            return allProducts;
        }

        public async Task<Item?> AddProduct(Item product)
        {
            CollectionReference collection = db.Collection("products");
            DocumentReference document = await collection.AddAsync(product);

            DocumentSnapshot snapshot = await document.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<Item?>();
            }

            return null;
        }

        public async Task<Item?> GetProduct(string productId)
        {
            DocumentReference document = db.Collection("products").Document(productId);
            DocumentSnapshot snapshot = await document.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Item? product = snapshot.ConvertTo<Item?>();
                return product;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateProduct(int productId, ProductDTO product)
        {
            Query query = db.Collection("products").WhereEqualTo("Id", productId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                await documentSnapshot.Reference.SetAsync(product, SetOptions.MergeAll);
            }
            else
            {
                throw new Exception($"No product found with Id {productId}");
            }
        }

        public async Task<Item?> DeleteProduct(int productId)
        {
            Query query = db.Collection("products").WhereEqualTo("Id", productId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                Item? itemToDelete = documentSnapshot.ConvertTo<Item?>();

                await documentSnapshot.Reference.DeleteAsync();

                return itemToDelete;
            }

            return null;
        }

        public async Task<Item?> IncrementProductQuantity(int productId)
        {
            Query query = db.Collection("products").WhereEqualTo("Id", productId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                Item? item = documentSnapshot.ConvertTo<Item?>();

                if (item != null)
                {
                    item.IncrementQuantity();
                    await documentSnapshot.Reference.SetAsync(item, SetOptions.MergeAll);
                }

                return item;
            }

            throw new Exception($"No product found with Id {productId}");
        }

        public async Task<Item?> DecrementProductQuantity(int productId)
        {
            Query query = db.Collection("products").WhereEqualTo("Id", productId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                Item? item = documentSnapshot.ConvertTo<Item?>();

                if (item != null)
                {
                    item.DecrementQuantity();
                    await documentSnapshot.Reference.SetAsync(item, SetOptions.MergeAll);
                }

                return item;
            }

            throw new Exception($"No product found with Id {productId}");
        }
    }

}
