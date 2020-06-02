using System.Collections.Generic;
using System.IO;

namespace AutoStructureGeneratorConsoleApp.AutoGeneratorClasses
{
    public class ClassesGenerator
    {
        public string _projectPath;
        public string _projectNameSpace;
        public List<string> _models;
        public string _contextName;
        public ClassesGenerator(string projectPath, string projectNameSpace, string contextName,
            List<string> models)
        {
            _projectNameSpace = projectNameSpace;
            _projectPath = projectPath;
            _models = models;
            _contextName = contextName;
        }
        public  void CreateModels()
        {
            foreach (var model in _models)
            {
                var streamWriter = new StreamWriter(path: $"{_projectPath}/_models/Db/{model}.cs");
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine($"using {_projectNameSpace}.Infrastructure.Helpers;");
                streamWriter.WriteLine($"namespace {_projectNameSpace}._models.Db");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine($"    public class {model} : ISoftDeleteModel");
                streamWriter.WriteLine("{\r\n public bool IsDeleted { get; set; }\r\n}\r\n}");
                streamWriter.Close();
            }
        }
        public  void CreateRepositoriesInterfaces()
        {
            var streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Interfaces/Repositories/IRepositoryBase.cs");
            streamWriter.WriteLine("using Microsoft.EntityFrameworkCore.Query;");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine("using System.Collections.Generic;");
            streamWriter.WriteLine("using System.Linq;");
            streamWriter.WriteLine("using System.Linq.Expressions;");
            streamWriter.WriteLine("using System.Threading.Tasks;");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Interfaces.Repositories");
            streamWriter.WriteLine("{\r\n    public interface IRepositoryBase<T> : IDisposable\r\n    {\r\n        IQueryable<T> FindAll();\r\n        IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate = null,\r\n            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,\r\n            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,\r\n            bool disableTracking = true);\r\n        Task<T> FindItemByCondition(Expression<Func<T, bool>> predicate = null,\r\n            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,\r\n            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,\r\n            bool disableTracking = true);\r\n        Task<bool> Insert(T entity);\r\n        Task<bool> InsertRange(List<T> entity);\r\n        Task<bool> Update(T entity);\r\n        Task<int> Commit();\r\n        Task<bool> Delete(T entity);\r\n        Task<bool> Rollback();\r\n    }\r\n}");
            streamWriter.Close();


             streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Interfaces/Repositories/IRepositoryWrapper.cs");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Interfaces.Repositories");
            streamWriter.WriteLine("{\r\n    public interface IRepositoryWrapper : IDisposable\r\n    {");
            foreach (var model in _models)
            {
                streamWriter.WriteLine($"        I{model}Repository {model}Repository"+" { get; }");
            }
            streamWriter.WriteLine("  }\r\n}");
            streamWriter.Close();

            foreach (var model in _models)
            {
                 streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Interfaces/Repositories/I{model}Repository.cs");
                streamWriter.WriteLine($"using {_projectNameSpace}.Models.Db;");
                streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Interfaces.Repositories");
                streamWriter.WriteLine($"{{\r\n    public interface I{model}Repository : IRepositoryBase<{model}>\r\n    {{\r\n    }}\r\n}}");
                streamWriter.Close();
            }

        }
        public  void CreateHelpers()
        {
            var streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Helpers/ClientResponse.cs");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Helpers");
            streamWriter.WriteLine("{\r\n    public class ClientResponse<T>\r\n    {\r\n        public ClientResponse(bool error, string message)\r\n        {\r\n            Error = error;\r\n            Message = message;\r\n        }\r\n        public ClientResponse(T data, int totalCount = 1)\r\n        {\r\n            Error = false;\r\n            Message = null;\r\n            Data = data;\r\n            TotalCount = totalCount;\r\n        }\r\n        public bool Error { get; set; }\r\n        public string Message { get; set; }\r\n        public T Data { get; set; }\r\n        public int TotalCount { get; set; }\r\n    }");
            streamWriter.Close();


             streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Helpers/ResponseError.cs");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Helpers");
            streamWriter.WriteLine("{\r\n    public class ResponseError\r\n    {\r\n        public ResponseError(int? errorTypeCode, string message, string errorDetails = null)\r\n        {\r\n            ErrorCode = errorTypeCode ?? 0;\r\n            Message = message;\r\n            Message = errorDetails;\r\n        }\r\n        public ResponseError(string message, string errorDetails = null)\r\n        {\r\n            Message = message;\r\n            DetailMessage = errorDetails;\r\n        }\r\n        public string Message { get; set; }\r\n        public string DetailMessage { get; set; }\r\n        public int ErrorCode { get; set; }\r\n        public override string ToString()\r\n        {\r\n            return Message;\r\n        }\r\n    }\r\n}");
            streamWriter.Close();


            streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Helpers/ServiceResponse.cs");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Helpers");
            streamWriter.WriteLine("{\r\n    public class ServiceResponse<T>\r\n    {\r\n        public ServiceResponse(T value)\r\n        {\r\n            Value = value;\r\n        }\r\n        public T Value { get; set; }\r\n        public string ResponseMessage { get; set; }\r\n        public ResponseError Error { get; set; }\r\n        public int TotalCount { get; set; }\r\n        public void SetSuccessResponse(T value, int totalCount = 1, string msg = null)\r\n        {\r\n            ResponseMessage = msg;\r\n            Value = value;\r\n            TotalCount = totalCount;\r\n        }\r\n    }\r\n}");
            streamWriter.Close();

            streamWriter = new StreamWriter(path: $"{_projectPath}/Infrastructure/Helpers/ISoftDeleteModel.cs");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.Infrastructure.Helpers");
            streamWriter.WriteLine("{\r\n    public interface ISoftDeleteModel\r\n    {\r\n        bool IsDeleted { get; set; }\r\n    }\r\n}");
            streamWriter.Close();
        }
        public  void CreateRepositories()
        {
            foreach (var model in _models)
            {
                var streamWriter = new StreamWriter(path: $"{_projectPath}/DAL/Repositories/{model}Repository.cs");
                streamWriter.WriteLine($"using {_projectNameSpace}.DAL.Abstraction;");
                streamWriter.WriteLine($"using {_projectNameSpace}.Infrastructure.Interfaces.Repositories;");
                streamWriter.WriteLine($"using {_projectNameSpace}.Models.Db;");
                streamWriter.WriteLine($"namespace {_projectNameSpace}.DAL.Repositories"+"{");
                streamWriter.WriteLine($"public class {model}Repository : RepositoryBase<{model}, {_contextName}Context>, I{model}Repository"+"{");
                streamWriter.WriteLine($"public {model}Repository({_contextName}Context repositoryContext) : base(repositoryContext)");
                streamWriter.WriteLine("  {\r\n        }\r\n    }\r\n}");
                streamWriter.Close();

            }
        }
        public  void CreateRepositoryWrapper()
        {
            var streamWriter = new StreamWriter(path: $"{_projectPath}/DAL/Abstraction/RepositoryWrapper.cs");
            streamWriter.WriteLine($"using {_projectNameSpace}.DAL.Repositories;");
            streamWriter.WriteLine($"using {_projectNameSpace}.Infrastructure.Interfaces.Repositories;");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.DAL.Abstraction"+"{");
            streamWriter.WriteLine("  public class RepositoryWrapper : IRepositoryWrapper\r\n    {");
            streamWriter.WriteLine($" public readonly {_contextName}Context _context;");
            streamWriter.WriteLine(" public bool _disposed;");
            foreach (var model in _models)
            {
                streamWriter.WriteLine($"public I{model}Repository _{model.ToLowerInvariant()}Repository;");
            }
            streamWriter.WriteLine($"public RepositoryWrapper({_contextName}Context context)");
            streamWriter.WriteLine(" {\r\n            _context = context;\r\n        }");
            streamWriter.WriteLine("public void Dispose()\r\n        {\r\n            Dispose(true);\r\n            GC.SuppressFinalize(this);\r\n        }");

            foreach (var model in _models)
            {
                streamWriter.WriteLine($"public I{model}Repository {model}Repository => _{model.ToLowerInvariant()}Repository ??= new {model}Repository(_context);");
            }

            streamWriter.WriteLine("  protected virtual void Dispose(bool disposing)\r\n        {\r\n            if (!_disposed)\r\n            {\r\n                if (disposing)\r\n                {\r\n                    _context?.Dispose();");
            foreach (var model in _models)
            {
                streamWriter.WriteLine($" _{model.ToLowerInvariant()}Repository?.Dispose();");
            }
            streamWriter.WriteLine("  }\r\n            }\r\n            _disposed = true;\r\n        }\r\n    }\r\n}");
            streamWriter.Close();
        }
        public  void CreateRepositoryBase()
        {
            var streamWriter = new StreamWriter(path: $"{_projectPath}/DAL/Abstraction/RepositoryBase.cs");

            streamWriter.WriteLine($"using {_projectNameSpace}.Infrastructure.Interfaces.Repositories;");
            streamWriter.WriteLine("using Microsoft.EntityFrameworkCore;\r\nusing Microsoft.EntityFrameworkCore.Query;\r\nusing System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Linq.Expressions;\r\nusing System.Threading.Tasks;");
            streamWriter.WriteLine($"namespace {_projectNameSpace}.DAL.Abstraction");
            streamWriter.WriteLine("{\r\n    public abstract class RepositoryBase<TModel, TDbContext> : IRepositoryBase<TModel> where TModel : class, new() where TDbContext : DbContext, new()\r\n    {\r\n        public bool _disposed;\r\n        protected TDbContext RepositoryContext { get; set; }\r\n        protected RepositoryBase(TDbContext repositoryContext)\r\n        {\r\n            RepositoryContext = repositoryContext;\r\n        }\r\n        public void Dispose()\r\n        {\r\n            Dispose(true);\r\n            GC.SuppressFinalize(this);\r\n        }\r\n        protected virtual void Dispose(bool disposing)\r\n        {\r\n            if (!_disposed)\r\n            {\r\n                if (disposing)\r\n                {\r\n                    RepositoryContext.Dispose();\r\n                }\r\n            }\r\n            _disposed = true;\r\n        }\r\n        public IQueryable<TModel> FindAll()\r\n        {\r\n            return RepositoryContext.Set<TModel>().AsNoTracking();\r\n        }\r\n        public IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> predicate = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null,\r\n            bool disableTracking = true)\r\n        {\r\n            var query = RepositoryContext.Set<TModel>().Where(predicate);\r\n            if (include != null)\r\n            {\r\n                query = include(query);\r\n            }\r\n            if (orderBy != null)\r\n            {\r\n                query = orderBy(query);\r\n            }\r\n            return disableTracking ? query.AsNoTracking().AsQueryable() : query.AsQueryable();\r\n        }\r\n        public async Task<TModel> FindItemByCondition(Expression<Func<TModel, bool>> predicate = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null,\r\n            bool disableTracking = true)\r\n        {\r\n            var query = RepositoryContext.Set<TModel>().Where(predicate);\r\n            if (include != null)\r\n            {\r\n                query = include(query);\r\n            }\r\n            if (orderBy != null)\r\n            {\r\n                query = orderBy(query);\r\n            }\r\n            if (disableTracking)\r\n            {\r\n                return await query.AsNoTracking().FirstOrDefaultAsync();\r\n            }\r\n            return await query.FirstOrDefaultAsync();\r\n        }\r\n        public async Task<bool> Insert(TModel entity)\r\n        {\r\n            await RepositoryContext.Set<TModel>().AddAsync(entity);\r\n            return true;\r\n        }\r\n        public async Task<bool> InsertRange(List<TModel> entity)\r\n        {\r\n            await RepositoryContext.Set<TModel>().AddRangeAsync(entity);\r\n            return true;\r\n        }\r\n        public async Task<bool> Update(TModel entity)\r\n        {\r\n            RepositoryContext.Set<TModel>().Update(entity);\r\n            return true;\r\n        }\r\n        public async Task<int> Commit()\r\n        {\r\n            return await RepositoryContext.SaveChangesAsync();\r\n        }\r\n        public async Task<bool> Delete(TModel entity)\r\n        {\r\n            RepositoryContext.Set<TModel>().Remove(entity);\r\n            return true;\r\n        }\r\n        public async Task<bool> Rollback()\r\n        {\r\n            RepositoryContext.ChangeTracker.Entries().ToList().ForEach(async x => await x.ReloadAsync());\r\n            return true;\r\n        }\r\n    }\r\n");

            streamWriter.Close();

        }
        public  void CreateContext()
        {
            //Open Context File
            var streamWriter = new StreamWriter(path: $"{_projectPath}/DAL/{_contextName}Context.cs");
            //Write Using
            streamWriter.WriteLine("using Microsoft.EntityFrameworkCore;");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine("using System.Linq;");
            streamWriter.WriteLine("using System.Reflection;");
            streamWriter.WriteLine("using System.Threading;");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine("using System.Threading.Tasks;");
            streamWriter.WriteLine($"using {_projectNameSpace}._models.Db;");

            //Start Context Structre
            streamWriter.WriteLine($"namespace {_projectNameSpace}.DAL");
            streamWriter.WriteLine("{");
            streamWriter.WriteLine($"public class {_contextName}Context : DbContext"+"{");
            streamWriter.WriteLine($"public {_contextName}Context(DbContextOptions<{_contextName}Context> options) : base(options)"+"{}");
         
            //Use All Db _models As Tables
            foreach (var model in _models)
            {
                streamWriter.WriteLine($"public DbSet<{model}> {model}s "+"{ get; set; }");

            }
            streamWriter.WriteLine($"public {_contextName}Context()"+"{}");

            //Override OnConfiguring method
            streamWriter.WriteLine("protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}");
          

            //Write SoftDelete methods
            streamWriter.WriteLine("protected override void OnModelCreating(ModelBuilder modelBuilder)\r\n        {\r\n            base.OnModelCreating(modelBuilder);\r\n            foreach (var type in modelBuilder.Model.GetEntityTypes())\r\n            {\r\n                if (typeof(ISoftDeleteModel).IsAssignableFrom(type.ClrType))\r\n                {\r\n                    modelBuilder.SetSoftDeleteFilter(type.ClrType);\r\n                }\r\n            }\r\n        }\r\n        public override int SaveChanges(bool acceptAllChangesOnSuccess)\r\n        {\r\n            OnBeforeSaving();\r\n            return base.SaveChanges(acceptAllChangesOnSuccess);\r\n        }\r\n        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,\r\n            CancellationToken cancellationToken = default(CancellationToken))\r\n        {\r\n            OnBeforeSaving();\r\n            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);\r\n        }\r\n        public void OnBeforeSaving()\r\n        {\r\n            foreach (var entry in ChangeTracker.Entries())\r\n            {\r\n                switch (entry.State)\r\n                {\r\n                    case EntityState.Added:\r\n                        entry.CurrentValues[\"IsDeleted\"] = false;\r\n                        break;\r\n                    case EntityState.Deleted:\r\n                        entry.State = EntityState.Modified;\r\n                        entry.CurrentValues[\"IsDeleted\"] = true;\r\n                        break;\r\n                }\r\n            }\r\n        }\r\n    }\r\n    public static class EFFilterExtensions\r\n    {\r\n        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)\r\n        {\r\n            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)\r\n                .Invoke(null, new object[] { modelBuilder });\r\n        }\r\n        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFFilterExtensions)\r\n            .GetMethods(BindingFlags.Public | BindingFlags.Static)\r\n            .Single(t => t.IsGenericMethod && t.Name == \"SetSoftDeleteFilter\");\r\n        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)\r\n            where TEntity : class, ISoftDeleteModel\r\n        {\r\n            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);\r\n        }\r\n    }");
           
            //close file
            streamWriter.Close();
        }
    }
}