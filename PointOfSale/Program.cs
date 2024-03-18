// See https://aka.ms/new-console-template for more information

using PointOfSale;

string filePath = @"C:\Users\arthu\OneDrive\Área de Trabalho\data-pos-pos.csv";

PointOfSaleMain pointOfSaleMain = new PointOfSaleMain(filePath);
pointOfSaleMain.RunMenu();