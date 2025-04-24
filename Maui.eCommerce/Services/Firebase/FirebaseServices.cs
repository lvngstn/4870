using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Library.eCommerce.DTO;

public class FirebaseService
{
    public FirestoreDb db;

    public FirebaseService()
    {
        // Initialize Firebase Admin SDK
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.GetApplicationDefault(),
            ProjectId = "project-1267165130420421013"
        });

        // Initialize Firestore
        db = FirestoreDb.Create("project-1267165130420421013");
    }

    public async Task<string> AddProduct(ProductDTO product)
    {
        CollectionReference collection = db.Collection("products");
        DocumentReference document = await collection.AddAsync(product);
        return document.Id;
    }

    public async Task<ProductDTO?> GetProduct(string productId)
    {
        DocumentReference document = db.Collection("products").Document(productId);
        DocumentSnapshot snapshot = await document.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            ProductDTO product = snapshot.ConvertTo<ProductDTO>();
            return product;
        }
        else
        {
            return null;
        }
    }

    public async Task UpdateProduct(string productId, ProductDTO product)
    {
        DocumentReference document = db.Collection("products").Document(productId);
        await document.SetAsync(product, SetOptions.MergeAll);
    }

    public async Task DeleteProduct(string productId)
    {
        DocumentReference document = db.Collection("products").Document(productId);
        await document.DeleteAsync();
    }
}
