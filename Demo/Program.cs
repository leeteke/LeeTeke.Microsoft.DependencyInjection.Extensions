// See https://aka.ms/new-console-template for more information
using Demo.Dll;
using Demo.Interfaces;
using Demo.Services;
using LeeTeke.Microsoft.DependencyInjection.Extensions.AOT;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();
services.AddFromAssembliyAOT();

var ioc = services.BuildServiceProvider();


//Singleton
var singleton_1 = ioc.GetService<ISingletonTest>();
var singleton_2 = ioc.GetService<ISingletonTest>();

Console.WriteLine($"{nameof(singleton_1)}：{singleton_1.Msg}");
Console.WriteLine($"{nameof(singleton_2)}：{singleton_2.Msg}");
Console.WriteLine();


//Transient
var transient_1 = ioc.GetService<ITransientTest>();
var transient_2 = ioc.GetService<ITransientTest>();

Console.WriteLine($"{nameof(transient_1)}：{transient_1.Msg}");
Console.WriteLine($"{nameof(transient_2)}：{transient_2.Msg}");
Console.WriteLine();

//Muliple
var muliple_A = ioc.GetService<IMultipleTestA>();
var muliple_B = ioc.GetService<IMultipleTestB>();

Console.WriteLine($"{nameof(muliple_A)}：{muliple_A.Msg_A}");
Console.WriteLine($"{nameof(muliple_B)}：{muliple_B.Msg_B}");
Console.WriteLine();

//NoInterface
//此处设置的为Singleton
var noInterface_1 = ioc.GetService<NoInterfaceService>();
var noInterface_2 = ioc.GetService<NoInterfaceService>();

Console.WriteLine($"{nameof(noInterface_1)}：{noInterface_1.Msg}");
Console.WriteLine($"{nameof(noInterface_2)}：{noInterface_2.Msg}");
Console.WriteLine();


var dllTest = ioc.GetService<IDllTest>();
Console.WriteLine($"{nameof(dllTest)}：{dllTest.Msg}");


Console.ReadLine();








