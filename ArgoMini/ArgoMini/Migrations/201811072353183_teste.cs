namespace odesempre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
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
                        ModeloNota = c.String(),
                        Serie = c.String(),
                        NumeroNota = c.String(),
                    })
                .PrimaryKey(t => t.EstabelecimentoId);
            
            CreateTable(
                "dbo.Impressoras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        SerialHd = c.String(),
                        Imprimindo = c.Boolean(nullable: false),
                        Nome = c.String(),
                        DataHoraUltimaImpressao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MercadoriaEstoques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MercadoriaId = c.Int(nullable: false),
                        EstoqueAtual = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mercadorias", t => t.MercadoriaId, cascadeDelete: true)
                .Index(t => t.MercadoriaId);
            
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
                        CodigoBarras = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cest = c.String(),
                        UnidadeMedida = c.String(),
                    })
                .PrimaryKey(t => t.MercadoriaId);
            
            CreateTable(
                "dbo.NotaFiscalCompras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        Chave = c.String(),
                        Serie = c.Int(nullable: false),
                        FornecedorId = c.Int(nullable: false),
                        DataEmissao = c.DateTime(nullable: false),
                        DataEntrada = c.DateTime(nullable: false),
                        Cnpj = c.String(),
                        NomeFornecedor = c.String(),
                        Situacao = c.Int(nullable: false),
                        ValorTotalNota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Xml = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotaFiscalCompraItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotaFiscalCompraId = c.Int(nullable: false),
                        MercadoriaId = c.Int(nullable: false),
                        CodigoMercadoriaImportada = c.String(),
                        CodigoBarrasMercadoriaImportada = c.String(),
                        DescricaoMercadoriaImportada = c.String(),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoCusto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMercadoria = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cest = c.String(),
                        Ncm = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mercadorias", t => t.MercadoriaId, cascadeDelete: true)
                .ForeignKey("dbo.NotaFiscalCompras", t => t.NotaFiscalCompraId, cascadeDelete: true)
                .Index(t => t.NotaFiscalCompraId)
                .Index(t => t.MercadoriaId);
            
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
                        ChaveNota = c.String(),
                        AutorizacaoNumeroProtocolo = c.String(),
                        AutorizacaoDataHoraProtocolo = c.DateTime(),
                        CancelamentoNumeroProtocolo = c.String(),
                        CancelamentoDataHoraProtocolo = c.DateTime(),
                        InutilizacaoNumeroProtocolo = c.String(),
                        InutilizacaoDataHoraProtocolo = c.DateTime(),
                        DenegacaoNumeroProtocolo = c.String(),
                        DenegacaoDataHoraProtocolo = c.DateTime(),
                        XmlCancelado = c.String(),
                        XmlAutorizadoCompactado = c.Boolean(nullable: false),
                        XmlAutorizadoPersistido = c.String(),
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
            DropForeignKey("dbo.NotaFiscalCompraItems", "NotaFiscalCompraId", "dbo.NotaFiscalCompras");
            DropForeignKey("dbo.NotaFiscalCompraItems", "MercadoriaId", "dbo.Mercadorias");
            DropForeignKey("dbo.MercadoriaEstoques", "MercadoriaId", "dbo.Mercadorias");
            DropIndex("dbo.NotaFiscalSaidaItems", new[] { "MercadoriaId" });
            DropIndex("dbo.NotaFiscalSaidaItems", new[] { "NotaFiscalSaidaId" });
            DropIndex("dbo.NotaFiscalCompraItems", new[] { "MercadoriaId" });
            DropIndex("dbo.NotaFiscalCompraItems", new[] { "NotaFiscalCompraId" });
            DropIndex("dbo.MercadoriaEstoques", new[] { "MercadoriaId" });
            DropTable("dbo.Pessoas");
            DropTable("dbo.NotaFiscalSaidas");
            DropTable("dbo.NotaFiscalSaidaItems");
            DropTable("dbo.NotaFiscalCompraItems");
            DropTable("dbo.NotaFiscalCompras");
            DropTable("dbo.Mercadorias");
            DropTable("dbo.MercadoriaEstoques");
            DropTable("dbo.Impressoras");
            DropTable("dbo.Estabelecimentoes");
        }
    }
}
