using Aspose.Cells;
using Microsoft.Win32;
using MyStore.Helpers;
using MyStore.Models;
using MyStore.Views;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MyStore.ViewModels
{
    public class InventoryUserControlViewModel
    {
        public InventoryUserControlModel model { get; set; }
        public ICommand ExcelImport { get; set; }
        public ICommand ReloadProducts { get; set; }
        public ICommand GoNextPage { get; set; }
        public ICommand GoPreviousPage { get; set; }
        public ICommand GoFirstPage { get; set; }
        public ICommand GoLastPage { get; set; }
        public ICommand SearchByName { get; set; }
        public ICommand PopupAddCategory { get; set; }
        public ICommand AddCategory { get; set; }
        public ICommand DeleteCategory { get; set; }
        public ICommand PopupUpdateCategory { get; set; }
        public ICommand UpdateCategory { get; set; }
        public ICommand PopupAddProduct { get; set; }
        public ICommand AddProduct { get; set; }
        public ICommand DeleteProduct { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand PopupUpdateProduct { get; set; }
        public InventoryUserControlViewModel()
        {
            
            model = new InventoryUserControlModel
            {
                categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories),
                pagingInfo = new PagingInfo
                {
                    totalItem = DataProvider.instance.database.Products.Count(),
                    itemPerPage = 6,
                    currentPage = 1
                }
            };

            ExcelImport = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                var dialog = new OpenFileDialog();

                if (dialog.ShowDialog() == true)
                {
                    var fileName = dialog.FileName;
                    var workbook = new Workbook(fileName);

                    foreach (var worksheet in workbook.Worksheets)
                    {
                        var row = 3;
                        var newCategory = new Category
                        {
                            CategoryID = NextCategoryIDGenerate(),
                            CategoryCode = worksheet.Name.Substring(0, 2).ToUpper(),
                            CategoryName = worksheet.Name,
                            CategoryImage = ImageToBitArray($"{Path.GetDirectoryName(fileName)}\\ImageData\\{worksheet.Cells["A1"].StringValue}")
                        };

                        DataProvider.instance.database.Categories.Add(newCategory);

                        while (worksheet.Cells[$"B{row}"].Value != null)
                        {
                            var newProduct = new Product
                            {
                                ProductID = NextProductIDGenerate(),
                                SKU = worksheet.Cells[$"B{row}"].StringValue,
                                ProductName = worksheet.Cells[$"C{row}"].StringValue,
                                ProductCategory = newCategory.CategoryID,
                                Price = worksheet.Cells[$"D{row}"].IntValue,
                                Total = worksheet.Cells[$"E{row}"].IntValue,
                                Sold = worksheet.Cells[$"F{row}"].IntValue,
                                Remain = worksheet.Cells[$"G{row}"].IntValue,
                                Size = (short)worksheet.Cells[$"H{row}"].IntValue,
                                Color = worksheet.Cells[$"I{row}"].StringValue,
                                Description = worksheet.Cells[$"J{row}"].StringValue
                            };

                            DataProvider.instance.database.Products.Add(newProduct);

                            var imageNames = worksheet.Cells[$"K{row}"].StringValue;
                            var imageNamesSplited = imageNames.Split(';');

                            foreach (var imageName in imageNamesSplited)
                            {
                                var newImage = new Image
                                {
                                    ImageID = NextImageIDGenerate(),
                                    ImageProduct = newProduct.ProductID,
                                    ImageData = ImageToBitArray($"{Path.GetDirectoryName(fileName)}\\ImageData\\{imageName}")
                                };

                                DataProvider.instance.database.Images.Add(newImage);
                            }

                            row++;
                        };

                        DataProvider.instance.database.SaveChanges();

                        model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                        model.pagingInfo.totalItem = DataProvider.instance.database.Products.Count();
                        target.categoryComboBox.SelectedIndex = 0;
                    }
                }
            });

            ReloadProducts = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.productListView.ItemsSource != null)
                {
                    target.searchBox.Clear();
                    model.collectionView = (CollectionView)CollectionViewSource.GetDefaultView(target.productListView.ItemsSource);
                    model.collectionView.Filter = null;
                    model.pagingInfo.filteredItem = target.productListView.Items.Count;
                    model.pagingInfo.totalPage = (target.productListView.Items.Count + model.pagingInfo.itemPerPage - 1) / model.pagingInfo.itemPerPage;
                    if (model.pagingInfo.totalPage == 0)
                        model.pagingInfo.currentPage = 0;
                    else
                        model.pagingInfo.currentPage = 1;
                    CollectionViewUpdate(target.productListView.ItemsSource);
                    target.productListView.SelectedIndex = 0;
                }
            });

            GoNextPage = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (model.pagingInfo.currentPage < model.pagingInfo.totalPage)
                {
                    model.pagingInfo.currentPage++;
                    model.collectionView.Filter = null;
                    CollectionViewUpdate(target.productListView.ItemsSource);
                    target.productListView.SelectedIndex = 0;
                }
            });

            GoPreviousPage = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (model.pagingInfo.currentPage > 1)
                {
                    model.pagingInfo.currentPage--;
                    model.collectionView.Filter = null;
                    CollectionViewUpdate(target.productListView.ItemsSource);
                    target.productListView.SelectedIndex = 0;
                }
            });

            GoFirstPage = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (model.pagingInfo.currentPage != 1)
                {
                    model.pagingInfo.currentPage = 1;
                    model.collectionView.Filter = null;
                    CollectionViewUpdate(target.productListView.ItemsSource);
                    target.productListView.SelectedIndex = 0;
                }
            });

            GoLastPage = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (model.pagingInfo.currentPage != model.pagingInfo.totalPage)
                {
                    model.pagingInfo.currentPage = model.pagingInfo.totalPage;
                    model.collectionView.Filter = null;
                    CollectionViewUpdate(target.productListView.ItemsSource);
                    target.productListView.SelectedIndex = 0;
                }
            });

            SearchByName = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.productListView.ItemsSource != null)
                {
                    model.collectionView = (CollectionView)CollectionViewSource.GetDefaultView(target.productListView.ItemsSource);
                    model.collectionView.Filter = new Predicate<object>(product => ((Product)product).ProductName.Contains(target.searchBox.Text));
                    model.pagingInfo.totalPage = (target.productListView.Items.Count + model.pagingInfo.itemPerPage - 1) / model.pagingInfo.itemPerPage;
                    if (model.pagingInfo.totalPage == 0)
                        model.pagingInfo.currentPage = 0;
                    else
                        model.pagingInfo.currentPage = 1;
                }
            });

            PopupAddCategory = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.addCategoryToggleButton.IsChecked == true)
                    target.addCategoryPopup.IsOpen = true;
                else
                {
                    target.addCategoryPopup.IsOpen = false;
                    target.popupAddCategoryTextBox.Clear();
                }
            });

            AddCategory = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                target.addCategoryPopup.IsOpen = false;
                target.addCategoryToggleButton.IsChecked = false;

                if (string.IsNullOrEmpty(target.popupAddCategoryTextBox.Text) || string.IsNullOrWhiteSpace(target.popupAddCategoryTextBox.Text))
                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Category name cannot be empty");
                else
                {
                    var newCategory = new Category
                    {
                        CategoryID = NextCategoryIDGenerate(),
                        CategoryCode = target.popupAddCategoryTextBox.Text.Substring(0, 2).ToUpper(),
                        CategoryName = target.popupAddCategoryTextBox.Text
                    };

                    DataProvider.instance.database.Categories.Add(newCategory);
                    DataProvider.instance.database.SaveChanges();

                    model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                    target.categoryComboBox.SelectedIndex = target.categoryComboBox.Items.Count - 1;

                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Add new category '{target.popupAddCategoryTextBox.Text}' successfully");
                }
            });

            DeleteCategory = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.categoryComboBox.SelectedItem != null)
                {
                    MyMessageBox.ShowChosen(Application.Current.MainWindow, $"Are you sure you want to delete the category '{target.categoryComboBox.Text}'?", ChooseYesNo);

                    if (model.chosen)
                    {
                        DataProvider.instance.database.Categories.Remove((Category)target.categoryComboBox.SelectedItem);
                        DataProvider.instance.database.SaveChanges();

                        model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                        target.categoryComboBox.SelectedIndex = 0;

                        target.categoryComboBox.SelectedIndex = 0;
                        MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Category deleted successfully");
                    }
                }
            });

            PopupUpdateCategory = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.updateCategoryToggleButton.IsChecked == true)
                    target.updateCategoryPopup.IsOpen = true;
                else
                    target.updateCategoryPopup.IsOpen = false;
            });

            UpdateCategory = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                target.updateCategoryPopup.IsOpen = false;
                target.updateCategoryToggleButton.IsChecked = false;

                if (string.IsNullOrEmpty(target.popupUpdateCategoryTextBox.Text) || string.IsNullOrWhiteSpace(target.popupUpdateCategoryTextBox.Text))
                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Category name cannot be empty");
                else
                {
                    DataProvider.instance.database.Categories.Find(target.categoryComboBox.SelectedValue).CategoryName = target.popupUpdateCategoryTextBox.Text;
                    DataProvider.instance.database.SaveChanges();

                    var selectedIndex = target.categoryComboBox.SelectedIndex;
                    model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                    target.categoryComboBox.SelectedIndex = -1;
                    target.categoryComboBox.SelectedIndex = selectedIndex;

                    MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Update category successfully");
                }
            });

            PopupAddProduct = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.addProductToggleButton.IsChecked == true)
                    target.addProductPopup.IsOpen = true;
                else
                {
                    target.popupAddProductNameTextBox.Clear();
                    target.popupAddProductColorTextBox.Clear();
                    target.popupAddProductPriceTextBox.Clear();
                    target.popupAddProductCategoryComboBox.SelectedIndex = 0;
                    target.popupAddProductTotalTextBox.Clear();
                    target.popupAddProductSoldTextBox.Clear();
                    target.popupAddProductRemainTextBox.Clear();
                    target.popupAddProductDescriptionTextBox.Clear();
                    target.addProductPopup.IsOpen = false;
                }
            });

            AddProduct = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                target.addProductPopup.IsOpen = false;
                target.addProductToggleButton.IsChecked = false;

                var prefix = DataProvider.instance.database.Categories.Find(target.popupAddProductCategoryComboBox.SelectedValue).CategoryCode;
                var mid = string.Empty;
                var number = (DataProvider.instance.database.Products
                    .Select(pd => pd)
                    .Where(pd => pd.ProductCategory == (int)target.popupAddProductCategoryComboBox.SelectedValue)
                    .ToList().Count + 1).ToString();

                for (int i = 0; i < (3 - number.Length); i++)
                    mid += "0";

                var newProduct = new Product
                {
                    ProductID = NextProductIDGenerate(),
                    SKU = prefix + mid + number,
                    ProductName = target.popupAddProductNameTextBox.Text,
                    ProductCategory = (int)target.popupAddProductCategoryComboBox.SelectedValue,
                    Color = target.popupAddProductColorTextBox.Text,
                    Price = int.Parse(target.popupAddProductPriceTextBox.Text),
                    Total = int.Parse(target.popupAddProductTotalTextBox.Text),
                    Sold = int.Parse(target.popupAddProductSoldTextBox.Text),
                    Remain = int.Parse(target.popupAddProductRemainTextBox.Text),
                    Description = target.popupAddProductDescriptionTextBox.Text
                };

                DataProvider.instance.database.Products.Add(newProduct);
                DataProvider.instance.database.SaveChanges();

                var selectedIndex = target.popupAddProductCategoryComboBox.SelectedIndex;
                model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                target.categoryComboBox.SelectedIndex = -1;
                target.categoryComboBox.SelectedIndex = selectedIndex;

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Add new product '{target.popupAddProductNameTextBox.Text}' successfully");

                target.popupAddProductNameTextBox.Clear();
                target.popupAddProductCategoryComboBox.SelectedIndex = 0;
                target.popupAddProductColorTextBox.Clear();
                target.popupAddProductPriceTextBox.Clear();
                target.popupAddProductTotalTextBox.Clear();
                target.popupAddProductSoldTextBox.Clear();
                target.popupAddProductRemainTextBox.Clear();
                target.popupAddProductDescriptionTextBox.Clear();
            });

            PopupUpdateProduct = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.updateProductToggleButton.IsChecked == true)
                    target.updateProductPopup.IsOpen = true;
                else
                    target.updateProductPopup.IsOpen = false;
            });

            UpdateProduct = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                target.updateProductPopup.IsOpen = false;
                target.updateProductToggleButton.IsChecked = false;

                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).ProductName = target.popupUpdateProductNameTextBox.Text;
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).ProductCategory = (int)target.popupUpdateProductCategoryComboBox.SelectedValue;
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Color = target.popupUpdateProductColorTextBox.Text;
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Price = int.Parse(target.popupUpdateProductPriceTextBox.Text);
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Total = int.Parse(target.popupUpdateProductTotalTextBox.Text);
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Sold = int.Parse(target.popupUpdateProductSoldTextBox.Text);
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Remain = int.Parse(target.popupUpdateProductRemainTextBox.Text);
                DataProvider.instance.database.Products.Find(int.Parse(target.detailIdTextBox.Text)).Description = target.popupUpdateProductDescriptionTextBox.Text;
                DataProvider.instance.database.SaveChanges();

                var selectedIndex = target.categoryComboBox.SelectedIndex;
                model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                target.categoryComboBox.SelectedIndex = -1;
                target.categoryComboBox.SelectedIndex = selectedIndex;

                MyMessageBox.ShowConfirmation(Application.Current.MainWindow, $"Update product successfully");
            });

            DeleteProduct = new RelayCommand<object>(p => p != null, p =>
            {
                var target = p as InventoryUserControlView;

                if (target.productListView.SelectedItem != null)
                {
                    MyMessageBox.ShowChosen(Application.Current.MainWindow, $"Are you sure you want to delete the product '{(target.productListView.SelectedItem as Product).ProductName}'?", ChooseYesNo);

                    if (model.chosen)
                    {
                        DataProvider.instance.database.Products.Remove((Product)target.productListView.SelectedItem);
                        DataProvider.instance.database.SaveChanges();

                        var selectedIndex = target.categoryComboBox.SelectedIndex;
                        model.categoryTable = new ObservableCollection<Category>(DataProvider.instance.database.Categories);
                        target.categoryComboBox.SelectedIndex = -1;
                        target.categoryComboBox.SelectedIndex = selectedIndex;

                        MyMessageBox.ShowConfirmation(Application.Current.MainWindow, "Product deleted successfully");
                    }
                }
            });
        }

        private void CollectionViewUpdate(object source)
        {
            model.collectionView.Filter = new Predicate<object>(product =>
                model.collectionView.IndexOf(product) < model.pagingInfo.itemPerPage * model.pagingInfo.currentPage &&
                model.collectionView.IndexOf(product) >= model.pagingInfo.itemPerPage * (model.pagingInfo.currentPage - 1));
        }

        private int NextCategoryIDGenerate()
        {
            var categoryTable = DataProvider.instance.database.Categories;

            for (int i = 1; true; i++)
            {
                if (categoryTable.Find(i) != null)
                {
                    if (categoryTable.Find(i).CategoryID == i)
                        continue;
                    continue;
                }
                else
                    return i;
            }
        }

        private int NextProductIDGenerate()
        {
            var productTable = DataProvider.instance.database.Products;

            for (int i = 1; true; i++)
            {
                if (productTable.Find(i) != null)
                {
                    if (productTable.Find(i).ProductID == i)
                        continue;
                    continue;
                }
                else
                    return i;
            }
        }

        private int NextImageIDGenerate()
        {
            var imageTable = DataProvider.instance.database.Images;

            for (int i = 1; true; i++)
            {
                if (imageTable.Find(i) != null)
                {
                    if (imageTable.Find(i).ImageID == i)
                        continue;
                    continue;
                }
                else
                    return i;
            }
        }

        private byte[] ImageToBitArray(string filePath)
        {
            byte[] result;

            using (var memoryStream = new MemoryStream())
            {
                var image = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                var encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
        }

        private void ChooseYesNo(int chosen)
        {
            model.chosen = chosen == 1 ? true : false;
        }
    }
}
