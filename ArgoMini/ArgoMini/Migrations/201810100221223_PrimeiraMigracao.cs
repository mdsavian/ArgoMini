namespace ArgoMini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraMigracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estabelecimentoes",
                c => new
                    {
                        EstabelecimentoId = c.Int(nullable: false, identity: true),
                        Cnpj = c.String(),
                        RazaoSocial = c.String(),
                        NomeFantasia = c.String(),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Bairro = c.String(),
                        CodigoMunicipio = c.String(),
                        NomeMunicipio = c.String(),
                        Uf = c.String(),
                        Cep = c.String(),
                        Fone = c.String(),
                        InscricaoEstadual = c.String(),
                        CodigoRegimeTributario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstabelecimentoId);
            
            CreateTable(
                "dbo.Mercadorias",
                c => new
                    {
                        MercadoriaId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        NcmId = c.String(),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoCusto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CodigoBarras = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MercadoriaId);
            
            CreateTable(
                "dbo.NotaFiscalSaidaItems",
                c => new
                    {
                        NotaFiscalSaidaItemId = c.Int(nullable: false, identity: true),
                        NotaFiscalSaidaId = c.Int(nullable: false),
                        MercadoriaId = c.Int(nullable: false),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMercadoria = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.NotaFiscalSaidaItemId)
                .ForeignKey("dbo.Mercadorias", t => t.MercadoriaId, cascadeDelete: true)
                .ForeignKey("dbo.NotaFiscalSaidas", t => t.NotaFiscalSaidaId, cascadeDelete: true)
                .Index(t => t.NotaFiscalSaidaId)
                .Index(t => t.MercadoriaId);
            
            CreateTable(
                "dbo.NotaFiscalSaidas",
                c => new
                    {
                        NotaFiscalSaidaId = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Serie = c.Int(nullable: false),
                        TipoDocumento = c.Int(nullable: false),
                        Situacao = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataEmissao = c.DateTime(),
                        VistaPrazo = c.Int(nullable: false),
                        CnpjCpf = c.String(),
                        ValorMercadorias = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantidadeItensNota = c.Int(nullable: false),
                        ValorTotalNota = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.NotaFiscalSaidaId);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        PessoaId = c.Int(nullable: false, identity: true),
                        TipoPessoa = c.Int(nullable: false),
                        RazaoSocial = c.String(),
                        NomeFantasia = c.String(),
                        CnpjCpf = c.String(),
                        InscricaoEstadual = c.String(),
                        Bairro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Endereco = c.String(),
                        Cep = c.String(),
                        Estado = c.String(),
                        Cidade = c.String(),
                    })
                .PrimaryKey(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotaFiscalSaidaItems", "NotaFiscalSaidaId", "dbo.NotaFiscalSaidas");
            DropForeignKey("dbo.NotaFiscalSaidaItems", "MercadoriaId", "dbo.Mercadorias");
            DropIndex("dbo.NotaFiscalSaidaItems", new[] { "MercadoriaId" });
            DropIndex("dbo.NotaFiscalSaidaItems", new[] { "NotaFiscalSaidaId" });
            DropTable("dbo.Pessoas");
            DropTable("dbo.NotaFiscalSaidas");
            DropTable("dbo.NotaFiscalSaidaItems");
            DropTable("dbo.Mercadorias");
            DropTable("dbo.Estabelecimentoes");
        }
    }
}
