﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp.CalculatorService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CalculatorService.CalculatorSoap")]
    public interface CalculatorSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Add", ReplyAction="*")]
        int Add(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Add", ReplyAction="*")]
        System.Threading.Tasks.Task<int> AddAsync(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Subtract", ReplyAction="*")]
        int Subtract(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Subtract", ReplyAction="*")]
        System.Threading.Tasks.Task<int> SubtractAsync(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Multiply", ReplyAction="*")]
        int Multiply(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Multiply", ReplyAction="*")]
        System.Threading.Tasks.Task<int> MultiplyAsync(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Divide", ReplyAction="*")]
        int Divide(int intA, int intB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Divide", ReplyAction="*")]
        System.Threading.Tasks.Task<int> DivideAsync(int intA, int intB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CalculatorSoapChannel : ConsoleApp.CalculatorService.CalculatorSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatorSoapClient : System.ServiceModel.ClientBase<ConsoleApp.CalculatorService.CalculatorSoap>, ConsoleApp.CalculatorService.CalculatorSoap {
        
        public CalculatorSoapClient() {
        }
        
        public CalculatorSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculatorSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Add(int intA, int intB) {
            return base.Channel.Add(intA, intB);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(int intA, int intB) {
            return base.Channel.AddAsync(intA, intB);
        }
        
        public int Subtract(int intA, int intB) {
            return base.Channel.Subtract(intA, intB);
        }
        
        public System.Threading.Tasks.Task<int> SubtractAsync(int intA, int intB) {
            return base.Channel.SubtractAsync(intA, intB);
        }
        
        public int Multiply(int intA, int intB) {
            return base.Channel.Multiply(intA, intB);
        }
        
        public System.Threading.Tasks.Task<int> MultiplyAsync(int intA, int intB) {
            return base.Channel.MultiplyAsync(intA, intB);
        }
        
        public int Divide(int intA, int intB) {
            return base.Channel.Divide(intA, intB);
        }
        
        public System.Threading.Tasks.Task<int> DivideAsync(int intA, int intB) {
            return base.Channel.DivideAsync(intA, intB);
        }
    }
}
