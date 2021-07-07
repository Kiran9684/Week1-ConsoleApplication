using MedicalStoreApplication.CustomExceptions;
using MedicalStoreApplication.Entities;
using MedicalStoreApplication.ToCompare;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace MedicalStoreApplication.Driver
{
    class TestMain : IMedicalStore
    {
        static List<Medicine> medicineList = new List<Medicine>();
        static List<Customer> customerList = new List<Customer>();
        static MedicalStore medicalStore;
        static ArrayList list = new ArrayList();

        public void addCustomer(Customer customer)
        {
            customerList.Add(customer);
        }

        public void addMedicines(Medicine medicine)
        {
            medicineList.Add(medicine);
        }
        public static void Main(string[] args)
        {
            TestMain test = new TestMain();
            bool flag = true;
            do
            {
                try
                {
                    IMedicalStore store = new TestMain();
                    int option = displayMainMenu();

                    switch (option)
                    {

                        case 1:
                            {
                                Medicine medicine = getMedicine();
                                test.addMedicines(medicine);

                                store.displayMedicines(medicineList);
                               
                                break;
                            }
                        case 2:
                            {
                                if (medicineList.Count != 0)
                                {
                                    Customer customer = getCustomer();
                                    test.addCustomer(customer);
                                    store.displayCustomers(customerList);
                                }
                                else
                                {
                                    Console.WriteLine("Enter 1 Medicine Atleast");
                                }

                                break;
                            }
                        case 3:
                            {
                                if (customerList.Count != 0)
                                {
                                    MedicalStore medStore = getstoreDeatails();
                                    medicalStore = medStore;
                                    //Display Medical store details
                                    store.displayStoreDetails(medicalStore);
                                }
                                else
                                {
                                    Console.WriteLine("Enter 1 Customer Atleast");
                                }


                                break;
                            }
                        case 4:
                            {
                                if (medicineList.Count != 0)
                                {
                                    CompareMedicines obj = new CompareMedicines();
                                    medicineList.Sort(obj); //I have used Icomparer
                                    foreach (Medicine med in medicineList)
                                    {
                                        Console.WriteLine(med.Name + " " + med.Quantity + " Nos " + med.PricePerMedicine + " Rs/Quantity");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Medecine List Is Empty .Add Medicines First");
                                }

                                break;
                            }
                        case 5:
                            {
                                if (customerList.Count != 0)
                                {
                                    customerList.Sort();
                                    foreach (Customer customer in customerList)
                                    {
                                        Console.WriteLine("Id = " + customer.Id + " Name = " + customer.Name + " ");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No Customers List In The Store . Add Customers First");
                                }
                                break;
                            }
                        case 6:
                            {
                                if (medicalStore != null && customerList.Count != 0 && medicineList.Count != 0)
                                {
                                    bool status = false;
                                    int id = 0;
                                    Customer customer = getDetails();
                                    foreach (Customer item in customerList)
                                    {
                                        status = item.Equals(customer);
                                        if (status == true)
                                        {
                                            id = item.Id;
                                            break;
                                        }
                                    }
                                    if (status == true)
                                    {
                                        Console.WriteLine("Customer Found");
                                        Customer customercopy = null;
                                        foreach (Customer item in customerList)
                                        {
                                            if (item.Id == id)
                                            {
                                                customercopy = item;
                                            }
                                        }
                                        //Display Bill 
                                        displayBill(customercopy);
                                        // save data To Text File
                                        saveBillToTextFile(customercopy);
                                        saveBillToTextFile2(customercopy);
                                        serializeData(customercopy);
                                        deserialize(customercopy);
                                        //method call to save data to excel 
                                        writedataToExcel(customercopy);
                                    

                                    }
                                    else
                                    {
                                        Console.WriteLine("Customer Not Found . Please Register");
                                        getCustomer();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("oops!! You have Skipped Some Steps please check ");
                                }
                                break;
                            }
                        case 7:
                            {
                                //put file exists validation
                                readData1();
                                readData2();
                                readData3();

                                break;
                            }
                        case 8:
                            {
                                Console.WriteLine("System Is Exiting .....");
                                flag = false;
                                break;
                            }
                    }
                }
                catch (MedicineNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (flag);


        }

       

        private static int displayMainMenu()
        {
            Console.WriteLine("*********Welcome To Medical Store***********");
            Console.WriteLine("Enter 1 To Add Medicines To System");
            Console.WriteLine("Enter 2 To Add Customers To System ");
            Console.WriteLine("Enter 3 To Add Medical Store Details To The System");
            Console.WriteLine("Enter 4 To Display Medicines List Sorted By Name ");
            Console.WriteLine("Enter 5 To Display Customer List Sorted By Id");
            Console.WriteLine("Enter 6 To Generate Bill For a Customer");
            Console.WriteLine("Enter 7 To Read Data From System Files");
            Console.WriteLine("Enter 8 To Exit ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
        private static Medicine getMedicine()
        {
            Medicine medicine = new Medicine();
            Console.WriteLine("Enter Medicine Name");
            medicine.Name = Console.ReadLine();
            Console.WriteLine("Enter Quantity");
            medicine.Quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Price Per Quantity");
            medicine.PricePerMedicine = (float)Convert.ToDouble(Console.ReadLine());
            return medicine;
        }

        private static Customer getCustomer()
        {

            Customer customer = new Customer();
            Console.WriteLine("Enter Customer Id");
            customer.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Customer Name");
            customer.Name = Console.ReadLine();

            Console.WriteLine("Enter Customer Age : Y/N");
            char ch1 = Convert.ToChar(Console.ReadLine());
            if(ch1 == 'Y' || ch1 == 'y')
            {
                Console.WriteLine("Enter Customer Age :");
                customer.Age = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Please Select Medicines . Enter Medicine Name ");
            bool flag = true;
            List<Medicine> CustomerMedicines = new List<Medicine>();
            do
            {
                foreach (Medicine item in medicineList)
                {
                    Console.WriteLine("**************************");
                    Console.WriteLine(item.Name + " " + item.PricePerMedicine + " ");
                    Console.WriteLine("**************************");
                }
                string mName = Console.ReadLine();
                foreach (Medicine item in medicineList)
                {
                    if (mName == item.Name)
                    {
                        CustomerMedicines.Add(item);
                        Console.WriteLine("Medicine Added To Cart");
                    }
                    else
                    {
                        //throw custom exception 
                        throw new MedicineNotFoundException("Entered Medicine Name is Invalid /Not Found");
                    }
                }
                Console.WriteLine("Want Add Another Medicine Y/N");
                char ch = Convert.ToChar(Console.ReadLine());
                if (ch == 'n' || ch == 'N')
                {
                    flag = false;
                }
            } while (flag);
            customer.Medicines = CustomerMedicines;
            return customer;
        }

        private static MedicalStore getstoreDeatails()
        {
            MedicalStore mstore = null;
            Console.WriteLine("Enter Medical Store Address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Store Owner Name");
            string name = Console.ReadLine();
            mstore = new MedicalStore(address, customerList, name);
            return mstore;
        }

        private static Customer getDetails()
        {
            Customer customer = new Customer();
            Console.WriteLine("Enter Id");
            customer.Id = Convert.ToInt32(Console.ReadLine());
            customer.Name = "";
            customer.Age = 0;
            customer.Medicines = null;
            return customer;

        }

        private static void displayBill(Customer customercopy)
        {
            Console.WriteLine("********BILL DETAILS********");
            Console.WriteLine("Medical Store");
            Console.WriteLine("Adress " + medicalStore.Address1);
            Console.WriteLine("Owner_Name " + medicalStore.OwnerName1);
            Console.WriteLine("PinCode " + MedicalStore.Pincode);
            Console.WriteLine("Customer Id " + customercopy.Id);
            Console.WriteLine("Customer Name " + customercopy.Name);
            Console.WriteLine("Customer Age " + customercopy.Age);
            Console.WriteLine("Medicine Details: ");
            float sum = 0;
            foreach (Medicine med in customercopy.Medicines)
            {
                Console.WriteLine("Medicine Name " + med.Name);
                Console.WriteLine("Medicine Nos " + med.Quantity);
                Console.WriteLine("Price Per Quantity " + med.PricePerMedicine);
                sum = sum + ((med.Quantity) * (med.PricePerMedicine));
            }
            Console.WriteLine("Total Price :" + sum + " Rupees");
        }

        private static void saveBillToTextFile(Customer customercopy)
        {
            FileStream stream = null;
            StreamWriter writer = null;
            try
            {
                Console.WriteLine("Enter File Name");
                string fileName = Console.ReadLine();
                string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(stream);
                if (stream.CanWrite)
                {
                    string address = Convert.ToString(medicalStore.Address1);
                    string ownerName = Convert.ToString(medicalStore.OwnerName1);
                    string pincode = Convert.ToString(MedicalStore.Pincode);
                    string str = "******Bill Details****** \nMedical Store \nAddress = " + address + "\nOwnerNAme = " + ownerName + "\nPincode = " + pincode;
                    writer.WriteLine(str);
                    string customerDetails = "Customer Name = " + Convert.ToString(customercopy.Name) + " Customer Age = " + Convert.ToString(customercopy.Age);
                    writer.WriteLine(customerDetails);
                    float sum = 0;
                    foreach (Medicine med in customercopy.Medicines)
                    {
                        string medicineDetails = "Medicine Name = " + Convert.ToString(med.Name) + " PricePerQuantity = " + Convert.ToString(med.PricePerMedicine) + " Quantity = " + Convert.ToString(med.Quantity);
                        sum = sum + ((med.Quantity) * (med.PricePerMedicine));
                        writer.WriteLine(medicineDetails);
                    }
                    string total = Convert.ToString(sum);
                    writer.WriteLine("Total Bill = " + total);

                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                writer.Close();
                stream.Close();
            }

        }
        private static void saveBillToTextFile2(Customer customercopy)
        {

            Console.WriteLine("Enter 2nd File Name");
            string fileName = Console.ReadLine();
            string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
            string address = Convert.ToString(medicalStore.Address1);
            string ownerName = Convert.ToString(medicalStore.OwnerName1);
            string pincode = Convert.ToString(MedicalStore.Pincode);
            string str = "******Bill Details****** \nMedical Store \nAddress = " + address + "\nOwnerNAme = " + ownerName + "\nPincode = " + pincode;
            File.WriteAllText(path, str);
            string customerDetails = "\nCustomer Name = " + Convert.ToString(customercopy.Name) + " Customer Age = " + Convert.ToString(customercopy.Age);
            File.AppendAllText(path, customerDetails);
            float sum = 0;
            foreach (Medicine med in customercopy.Medicines)
            {
                string medicineDetails = "\nMedicine Name = " + Convert.ToString(med.Name) + " PricePerQuantity = " + Convert.ToString(med.PricePerMedicine) + " Quantity = " + Convert.ToString(med.Quantity);
                sum = sum + ((med.Quantity) * (med.PricePerMedicine));
                File.AppendAllText(path, medicineDetails);
            }
            string total = Convert.ToString(sum);
            File.AppendAllText(path, total);
        }

        private static void readData1()
        {
            Console.WriteLine("Enter 1st File Name");
            string fileName = Console.ReadLine();
            string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
            FileStream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(stream2);
            if (stream2.CanRead)
            {
                Console.WriteLine("File Contents Are ");
                Console.WriteLine(read.ReadToEnd());
            }
            read.Close();
            stream2.Close();
        }
        private static void readData2()
        {

            Console.WriteLine("Enter 2nd File Name");
            string fileName = Console.ReadLine();
            string filePath = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
            Console.WriteLine(File.ReadAllText(filePath));
        }

        private static void serializeData(Customer customer)
        {


            list.Add(medicalStore.OwnerName1);
            list.Add(medicalStore.Address1);
            list.Add(MedicalStore.Pincode);
            list.Add(customer.Name);
            list.Add(customer.Age);
            foreach (Medicine item in customer.Medicines)
            {
                list.Add(item.Name);
                list.Add(item.Quantity);
            }

            Console.WriteLine("Writing to text file using serialization...");
            FileStream fs = null;
            try
            {
                Console.WriteLine("Enter 2nd File Name");
                string fileName = Console.ReadLine();
                string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

                foreach (var data in list)
                {
                    binaryFormatter.Serialize(fs, data);
                }

                Console.WriteLine("Serialization successful!");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "Something went wrong!");
            }
            finally
            {
                fs.Close();
            }
        }

        private static void deserialize(Customer customer)
        {
            Console.WriteLine("Enter 2nd File Name");
            string fileName = Console.ReadLine();
            Console.WriteLine("Reading from text file using deserialization...");
            FileStream? fs = null;
            try
            {
                string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\DataFiles" + fileName + ".txt";
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                for (int i = 0; i < list.Count; i++)
                {
                    Object data = binaryFormatter.Deserialize(fs);


                    Console.WriteLine(data);

                }

                Console.WriteLine("Deserialization successful!");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "Something went wrong!");
            }
            finally
            {
                fs.Close();
            }
        }

        private static void writedataToExcel(Customer customer)
        {

            Console.WriteLine("Writing to excel file using Interop...");
            Workbook? wb = null;
            Worksheet? ws = null;
            Application excel = new _Excel.Application();
            try
            {

                string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\Bill.xlsx";
                wb = excel.Workbooks.Add(Type.Missing);
                ws = wb.ActiveSheet;
                ws.Name = "Bill Details";
                int i = 1, j = 1;

                ((_Excel.Range)ws.Cells[i, j++]).Value = "Adress";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "OwnerName";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "CustomerName";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "MedcineName";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "Quantity";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "PricePerQuantity";
                ((_Excel.Range)ws.Cells[i, j++]).Value = "Total Price";


                i++;
                j = 1;

                ((_Excel.Range)ws.Cells[i, j++]).Value = medicalStore.Address1;
                ((_Excel.Range)ws.Cells[i, j++]).Value = medicalStore.OwnerName1;
                ((_Excel.Range)ws.Cells[i, j++]).Value = customer.Name;
                float sum = 0;
                foreach (Medicine item in customer.Medicines)
                {
                    ((_Excel.Range)ws.Cells[i, j++]).Value = item.Name;
                    ((_Excel.Range)ws.Cells[i, j++]).Value = item.Quantity;
                    ((_Excel.Range)ws.Cells[i, j++]).Value = item.PricePerMedicine;
                    sum = sum + (item.Quantity) * (item.PricePerMedicine);
                    i++;

                }
                 ((_Excel.Range)ws.Cells[i, j++]).Value = sum;

                wb.SaveAs(path);
                Console.WriteLine("Write to excel file successful!");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                wb.Close();
                excel.Quit();
            }

        }

        private static void readData3()
        {
            try
            {
                Console.WriteLine("Reading from excel file using Interop...");
                Workbook? wb = null;
                Worksheet? ws = null;
                Application excel = new _Excel.Application();
                try
                {
                    string path = @"C:\Users\KIRAN\source\repos\MedicalStoreApplication\MedicalStoreApplication\Bill.xlsx";
                    //string path = "";
                    wb = excel.Workbooks.Open(path);
                    ws = wb.ActiveSheet;
                    ws.Name = "Bill";
                    int z = 1, j = 1, i = 2, x = 1;

                    for (int k = 0; k < 8; k++)
                    {
                        Console.WriteLine(((_Excel.Range)ws.Cells[z, j++]).Value + " : " + ((_Excel.Range)ws.Cells[i, x++]).Value);

                    }


                }
                catch (Exception e)
                {
                    throw new InnerExceptionDemo("This is InnerException Demonstration", e);
                   // Console.WriteLine(e.Message);
                }
                finally
                {
                    if(wb != null)
                    {
                        wb.Close();
                    }
                  
                    excel.Quit();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Current Exception" + e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner Exception" + e.InnerException.Message);
                }
            }
           

        }




    }
}
