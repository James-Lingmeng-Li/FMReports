﻿/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


public partial class ReportingService
{
    public interface IAsync
    {
        /// <summary>
        /// A method definition looks like C code. It has a return type, arguments,
        /// and optionally a list of exceptions that it may throw. Note that argument
        /// lists and exception lists are specified using the exact same syntax as
        /// field lists in struct or exception definitions.
        /// </summary>
        Task pingAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ReportOutput> getBatchReportsAsync(string _reportName, string _date, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// This method has a oneway modifier. That means the client only makes
        /// a request and does not listen for any response at all. Oneway methods
        /// must be void.
        /// </summary>
        Task zipAsync(CancellationToken cancellationToken = default(CancellationToken));

    }


    public class Client : TBaseClient, IDisposable, IAsync
    {
        public Client(TProtocol protocol) : this(protocol, protocol)
        {
        }

        public Client(TProtocol inputProtocol, TProtocol outputProtocol) : base(inputProtocol, outputProtocol)
        {
        }
        public async Task pingAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await OutputProtocol.WriteMessageBeginAsync(new TMessage("ping", TMessageType.Call, SeqId), cancellationToken);

            var args = new pingArgs();

            await args.WriteAsync(OutputProtocol, cancellationToken);
            await OutputProtocol.WriteMessageEndAsync(cancellationToken);
            await OutputProtocol.Transport.FlushAsync(cancellationToken);

            var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
            if (msg.Type == TMessageType.Exception)
            {
                var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
                await InputProtocol.ReadMessageEndAsync(cancellationToken);
                throw x;
            }

            var result = new pingResult();
            await result.ReadAsync(InputProtocol, cancellationToken);
            await InputProtocol.ReadMessageEndAsync(cancellationToken);
            return;
        }

        public async Task<ReportOutput> getBatchReportsAsync(string _reportName, string _date, CancellationToken cancellationToken = default(CancellationToken))
        {
            await OutputProtocol.WriteMessageBeginAsync(new TMessage("getBatchReports", TMessageType.Call, SeqId), cancellationToken);

            var args = new getBatchReportsArgs();
            args._reportName = _reportName;
            args._date = _date;

            await args.WriteAsync(OutputProtocol, cancellationToken);
            await OutputProtocol.WriteMessageEndAsync(cancellationToken);
            await OutputProtocol.Transport.FlushAsync(cancellationToken);

            var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
            if (msg.Type == TMessageType.Exception)
            {
                var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
                await InputProtocol.ReadMessageEndAsync(cancellationToken);
                throw x;
            }

            var result = new getBatchReportsResult();
            await result.ReadAsync(InputProtocol, cancellationToken);
            await InputProtocol.ReadMessageEndAsync(cancellationToken);
            if (result.__isset.success)
            {
                return result.Success;
            }
            throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "getBatchReports failed: unknown result");
        }

        public async Task zipAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await OutputProtocol.WriteMessageBeginAsync(new TMessage("zip", TMessageType.Oneway, SeqId), cancellationToken);

            var args = new zipArgs();

            await args.WriteAsync(OutputProtocol, cancellationToken);
            await OutputProtocol.WriteMessageEndAsync(cancellationToken);
            await OutputProtocol.Transport.FlushAsync(cancellationToken);
        }
    }

    public class AsyncProcessor : ITAsyncProcessor
    {
        private IAsync _iAsync;

        public AsyncProcessor(IAsync iAsync)
        {
            if (iAsync == null) throw new ArgumentNullException(nameof(iAsync));

            _iAsync = iAsync;
            processMap_["ping"] = ping_ProcessAsync;
            processMap_["getBatchReports"] = getBatchReports_ProcessAsync;
            processMap_["zip"] = zip_ProcessAsync;
        }

        protected delegate Task ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken);
        protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

        public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot)
        {
            return await ProcessAsync(iprot, oprot, CancellationToken.None);
        }

        public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
        {
            try
            {
                var msg = await iprot.ReadMessageBeginAsync(cancellationToken);

                ProcessFunction fn;
                processMap_.TryGetValue(msg.Name, out fn);

                if (fn == null)
                {
                    await TProtocolUtil.SkipAsync(iprot, TType.Struct, cancellationToken);
                    await iprot.ReadMessageEndAsync(cancellationToken);
                    var x = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
                    await oprot.WriteMessageBeginAsync(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID), cancellationToken);
                    await x.WriteAsync(oprot, cancellationToken);
                    await oprot.WriteMessageEndAsync(cancellationToken);
                    await oprot.Transport.FlushAsync(cancellationToken);
                    return true;
                }

                await fn(msg.SeqID, iprot, oprot, cancellationToken);

            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public async Task ping_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
        {
            var args = new pingArgs();
            await args.ReadAsync(iprot, cancellationToken);
            await iprot.ReadMessageEndAsync(cancellationToken);
            var result = new pingResult();
            try
            {
                await _iAsync.pingAsync(cancellationToken);
                await oprot.WriteMessageBeginAsync(new TMessage("ping", TMessageType.Reply, seqid), cancellationToken);
                await result.WriteAsync(oprot, cancellationToken);
            }
            catch (TTransportException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error occurred in processor:");
                Console.Error.WriteLine(ex.ToString());
                var x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                await oprot.WriteMessageBeginAsync(new TMessage("ping", TMessageType.Exception, seqid), cancellationToken);
                await x.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteMessageEndAsync(cancellationToken);
            await oprot.Transport.FlushAsync(cancellationToken);
        }

        public async Task getBatchReports_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
        {
            var args = new getBatchReportsArgs();
            await args.ReadAsync(iprot, cancellationToken);
            await iprot.ReadMessageEndAsync(cancellationToken);
            var result = new getBatchReportsResult();
            try
            {
                result.Success = await _iAsync.getBatchReportsAsync(args._reportName, args._date, cancellationToken);
                await oprot.WriteMessageBeginAsync(new TMessage("getBatchReports", TMessageType.Reply, seqid), cancellationToken);
                await result.WriteAsync(oprot, cancellationToken);
            }
            catch (TTransportException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error occurred in processor:");
                Console.Error.WriteLine(ex.ToString());
                var x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                await oprot.WriteMessageBeginAsync(new TMessage("getBatchReports", TMessageType.Exception, seqid), cancellationToken);
                await x.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteMessageEndAsync(cancellationToken);
            await oprot.Transport.FlushAsync(cancellationToken);
        }

        public async Task zip_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
        {
            var args = new zipArgs();
            await args.ReadAsync(iprot, cancellationToken);
            await iprot.ReadMessageEndAsync(cancellationToken);
            try
            {
                await _iAsync.zipAsync(cancellationToken);
            }
            catch (TTransportException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error occurred in processor:");
                Console.Error.WriteLine(ex.ToString());
            }
        }

    }


    public partial class pingArgs : TBase
    {

        public pingArgs()
        {
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        default:
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("ping_args");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as pingArgs;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return true;
        }

        public override int GetHashCode()
        {
            int hashcode = 157;
            unchecked
            {
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("ping_args(");
            sb.Append(")");
            return sb.ToString();
        }
    }


    public partial class pingResult : TBase
    {

        public pingResult()
        {
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        default:
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("ping_result");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as pingResult;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return true;
        }

        public override int GetHashCode()
        {
            int hashcode = 157;
            unchecked
            {
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("ping_result(");
            sb.Append(")");
            return sb.ToString();
        }
    }


    public partial class getBatchReportsArgs : TBase
    {
        private string __reportName;
        private string __date;

        public string _reportName
        {
            get
            {
                return __reportName;
            }
            set
            {
                __isset._reportName = true;
                this.__reportName = value;
            }
        }

        public string _date
        {
            get
            {
                return __date;
            }
            set
            {
                __isset._date = true;
                this.__date = value;
            }
        }


        public Isset __isset;
        public struct Isset
        {
            public bool _reportName;
            public bool _date;
        }

        public getBatchReportsArgs()
        {
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        case 1:
                            if (field.Type == TType.String)
                            {
                                _reportName = await iprot.ReadStringAsync(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 2:
                            if (field.Type == TType.String)
                            {
                                _date = await iprot.ReadStringAsync(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        default:
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("getBatchReports_args");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                if (_reportName != null && __isset._reportName)
                {
                    field.Name = "_reportName";
                    field.Type = TType.String;
                    field.ID = 1;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteStringAsync(_reportName, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (_date != null && __isset._date)
                {
                    field.Name = "_date";
                    field.Type = TType.String;
                    field.ID = 2;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteStringAsync(_date, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as getBatchReportsArgs;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((__isset._reportName == other.__isset._reportName) && ((!__isset._reportName) || (System.Object.Equals(_reportName, other._reportName))))
              && ((__isset._date == other.__isset._date) && ((!__isset._date) || (System.Object.Equals(_date, other._date))));
        }

        public override int GetHashCode()
        {
            int hashcode = 157;
            unchecked
            {
                if (__isset._reportName)
                    hashcode = (hashcode * 397) + _reportName.GetHashCode();
                if (__isset._date)
                    hashcode = (hashcode * 397) + _date.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("getBatchReports_args(");
            bool __first = true;
            if (_reportName != null && __isset._reportName)
            {
                if (!__first) { sb.Append(", "); }
                __first = false;
                sb.Append("_reportName: ");
                sb.Append(_reportName);
            }
            if (_date != null && __isset._date)
            {
                if (!__first) { sb.Append(", "); }
                __first = false;
                sb.Append("_date: ");
                sb.Append(_date);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }


    public partial class getBatchReportsResult : TBase
    {
        private ReportOutput _success;

        public ReportOutput Success
        {
            get
            {
                return _success;
            }
            set
            {
                __isset.success = true;
                this._success = value;
            }
        }


        public Isset __isset;
        public struct Isset
        {
            public bool success;
        }

        public getBatchReportsResult()
        {
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        case 0:
                            if (field.Type == TType.Struct)
                            {
                                Success = new ReportOutput();
                                await Success.ReadAsync(iprot, cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        default:
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("getBatchReports_result");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();

                if (this.__isset.success)
                {
                    if (Success != null)
                    {
                        field.Name = "Success";
                        field.Type = TType.Struct;
                        field.ID = 0;
                        await oprot.WriteFieldBeginAsync(field, cancellationToken);
                        await Success.WriteAsync(oprot, cancellationToken);
                        await oprot.WriteFieldEndAsync(cancellationToken);
                    }
                }
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as getBatchReportsResult;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
        }

        public override int GetHashCode()
        {
            int hashcode = 157;
            unchecked
            {
                if (__isset.success)
                    hashcode = (hashcode * 397) + Success.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("getBatchReports_result(");
            bool __first = true;
            if (Success != null && __isset.success)
            {
                if (!__first) { sb.Append(", "); }
                __first = false;
                sb.Append("Success: ");
                sb.Append(Success == null ? "<null>" : Success.ToString());
            }
            sb.Append(")");
            return sb.ToString();
        }
    }


    public partial class zipArgs : TBase
    {

        public zipArgs()
        {
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        default:
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("zip_args");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as zipArgs;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return true;
        }

        public override int GetHashCode()
        {
            int hashcode = 157;
            unchecked
            {
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("zip_args(");
            sb.Append(")");
            return sb.ToString();
        }
    }
}
