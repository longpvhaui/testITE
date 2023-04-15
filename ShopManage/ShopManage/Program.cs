using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopManage
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            while (true)
            {
                var listProduct = InitData();
                DisplayMenu(listProduct);

                Console.ReadLine();
            }
        }


        //seed data 
        public static List<Product> InitData()
        {
            List<Product> listProduct = new List<Product>();
            Product ipad = new Product("ipd", "Super Ipad", 549.99);
            Product macbook = new Product("mbp", "MacBook pro", 1399.99);
            Product tivi = new Product("atv", "Apple Tivi", 109.99);
            Product agv = new Product("agv", "AGV adapter", 109.99);
            listProduct.Add(ipad);
            listProduct.Add(macbook);
            listProduct.Add(tivi);
            listProduct.Add(agv);
            return listProduct;
        }
        public static void DisplayMenu(List<Product> listProducts)
        {
      
            Console.WriteLine("===== CHƯƠNG TRÌNH QUẢN LÝ CỬA HÀNG =====");
            Console.WriteLine("1. Hiển thị danh sách sản phẩm");
            Console.WriteLine("2. Thêm sản phẩm");
            Console.WriteLine("3. Cập nhật giá sản phẩm");
            Console.WriteLine("4. Tính tiền và áp dụng khuyến mại");
            Console.WriteLine("5. Thoát chương trình");
            Console.Write("Vui lòng chọn một lựa chọn từ menu trên: ");

            while (true)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Hiển thị giá sản phẩm
                        DisplayProduct(listProducts);
                        break;
                    case "2":
                        // Thêm sản phẩm
                        Console.Write("Nhập mã sản phẩm: ");
                        string newId = Console.ReadLine();
                        if (string.IsNullOrEmpty(newId))
                        {
                            break;
                        }
                        Console.Write("Nhập tên sản phẩm: ");
                        string newName = Console.ReadLine();
                        if (string.IsNullOrEmpty(newName))
                        {
                            break;
                        }
                   
                        Console.Write("Nhập giá: ");
                        double newPrice = double.Parse(Console.ReadLine());

                        AddProduct(newId, newName, newPrice,listProducts);
                        break;
                    case "3":
                        // Cập nhật giá sản phẩm
                        Console.Write("Vui lòng nhập mã của sản phẩm: ");
                        string id = Console.ReadLine();
                        Console.Write("Vui lòng nhập giá mới của sản phẩm: ");
                        double price = double.Parse(Console.ReadLine());
                        UpdateProduct(id, price, listProducts);
                        break;
                    case "4":
                        // Tính tiền và áp dụng khuyến mại
                        Console.WriteLine("Chỗ này sẽ là scan , nhưng chúng ta giả lập cho nhập nhé =)) ");
                        Console.WriteLine("Nhập các mã sản phẩm ngăn cách nhau bằng dấu ,");
                        Console.Write("Bắt đầu nhập :   ");
                        string productId = Console.ReadLine();
                        CalculateTotalPrice(productId, listProducts);
                        break;
                    case "5":
                        // Thoát chương trình
                        Console.Write("Bai Bai  Đang thoát................ >< ");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }
            }
        }
        public static void DisplayProduct(List<Product> listProduct)
        {
            Console.Clear();
            Console.WriteLine("Bảng giá:");
            Console.WriteLine("| ID  |    TÊN MẶT HÀNG    |    ĐƠN GIÁ   |");
            foreach (var product in listProduct)
            {
                Console.WriteLine("| " + product.Id + " | " + product.Name.PadRight(18) + " | " + product.Price.ToString("C").PadRight(12) + " | ");
            }
            DisplayMenu(listProduct);
        }

        public static void AddProduct(string id, string name, double price, List<Product> listProduct)
        {
            Console.Clear();
            Product product = new Product(id, name, price);
            var proExits = listProduct.FirstOrDefault(x=>x.Id == id);
            if (proExits != null)
            {
                Console.WriteLine("Đã có sản phẩm tồn tại");
            }
            else if(string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Tên sản phẩm không được bổ trống");
            }
            else if (product != null)
            {
                listProduct.Add(product);
                Console.WriteLine($"Đã thêm sản phẩm có mã là :  {id} tên là {name} với giá {price} thành công.");
            }
            else
            {
                Console.WriteLine($"Thêm sản phẩm không thành công.");
            }
            DisplayMenu(listProduct);
        }
        public static void UpdateProduct(string id, double price, List<Product> listProduct)
        {
            Console.Clear();
            var product = listProduct.FirstOrDefault(x=>x.Id == id);
            if (product != null)
            {
                product.Price = price;
                Console.WriteLine($"Đã cập nhật giá cho sản phẩm có mã là :  {id} thành công.");
            }
            else
            {
                Console.WriteLine($"Sản phẩm có mã là : {id} không tồn tại trong hệ thống.");
            }
            DisplayMenu(listProduct);
        }
        public static void CalculateTotalPrice(string productId, List<Product> listProduct)
        {

            Console.Clear();
            string[] productCodes = productId.Split(',');
           

            // Tạo một dictionary để lưu số lượng các sản phẩm
             Dictionary<string, int> productCount = new Dictionary<string, int>();
            foreach (string code in productCodes)
            {
                if (productCount.ContainsKey(code))
                {
                    productCount[code]++;
                }
                else
                {
                    productCount[code] = 1;
                }
            }

            // Tính giá tiền cho từng sản phẩm
            double total = 0;
            var isHasMB = false;
            Console.WriteLine("Chi tiết đơn hàng");
            Console.WriteLine("| ID  | SỐ LƯỢNG |");
            foreach (KeyValuePair<string, int> entry in productCount)
            {
                string code = entry.Key;
                int count = entry.Value;
                if (code != "atv" && code != "ipd" && code != "mbp" && code != "agv")
                {
                    Console.WriteLine($"Không tồn tại sản phẩm này : {code}");
                }
                else
                {
                    Console.WriteLine($"|{code}   | {count}        |");
                }
                Product product = listProduct.Find(p => p.Id == code);
                if (product != null)
                {
                   
                    if (product.Id == "atv" && count >= 3)
                    {
                        total += product.Price * (count - count / 3);
                    }
                    else if (product.Id == "ipd" && count >= 4)
                    {
                        total = count * 499.99;
                    }
                    else if (product.Id == "mbp")
                    {
                        isHasMB = true;
                        total += product.Price * count;
                    }
                    else if (product.Id == "agv" && isHasMB)
                    {
                        continue;
                    }
                    else total += product.Price * count;

                }
        }

            // Hiển thị tổng tiền
            Console.WriteLine($"Tổng tiền: {total:C}");
            DisplayMenu(listProduct);
        }


    }
}
