using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CatalogoAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    //mapeamento das entidades
    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Categoria>? Categorias { get; set; }

    //personalizar o mapeamento
    protected override void OnModelCreating(ModelBuilder mb)
    {
        //fluentApi
        //categoria
        mb.Entity<Categoria>().HasKey(c => c.CategoriaId);
        mb.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
        mb.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(150).IsRequired();

        //produto
        mb.Entity<Produto>().HasKey(p => p.ProdutoId);
        mb.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();
        mb.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(150);
        mb.Entity<Produto>().Property(p => p.ImagemUrl).HasMaxLength(100);
        mb.Entity<Produto>().Property(p => p.Preco).HasPrecision(14, 2);

        //relacionamento
        mb.Entity<Produto>().HasOne<Categoria>(c => c.Categoria).WithMany(p => p.Produtos).HasForeignKey(c => c.CategoriaId);
    }
}
