// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

namespace Podaci.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220302210315_dodajpropertyDG")]
    partial class dodajpropertyDG
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Podaci.Klase.Cijena", b =>
                {
                    b.Property<int>("CijenaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GradDolaskaGradID")
                        .HasColumnType("int");

                    b.Property<int>("GradPolaskaGradID")
                        .HasColumnType("int");

                    b.Property<float>("JednosmijernaKartaCijena")
                        .HasColumnType("real");

                    b.Property<float>("PovratnaKartaCijena")
                        .HasColumnType("real");

                    b.HasKey("CijenaID");

                    b.HasIndex("GradDolaskaGradID");

                    b.HasIndex("GradPolaskaGradID");

                    b.ToTable("Cijena");
                });

            modelBuilder.Entity("Podaci.Klase.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumKreiranja")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KupacId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sadrzaj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KupacId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Podaci.Klase.Grad", b =>
                {
                    b.Property<int>("GradID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradID");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("Podaci.Klase.Karta", b =>
                {
                    b.Property<int>("KartaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<string>("DatumDolaska")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumKupovine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumPolaska")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DolazisteID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAktivna")
                        .HasColumnType("bit");

                    b.Property<int?>("KKarticaID")
                        .HasColumnType("int");

                    b.Property<string>("KupacId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Kupac_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NazivLinije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PolazisteID")
                        .HasColumnType("int");

                    b.Property<int>("TipKarteID")
                        .HasColumnType("int");

                    b.Property<int>("VrstaPopustaID")
                        .HasColumnType("int");

                    b.HasKey("KartaID");

                    b.HasIndex("DolazisteID");

                    b.HasIndex("KKarticaID");

                    b.HasIndex("KupacId");

                    b.HasIndex("PolazisteID");

                    b.HasIndex("TipKarteID");

                    b.HasIndex("VrstaPopustaID");

                    b.ToTable("Karta");
                });

            modelBuilder.Entity("Podaci.Klase.Korisnik", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Korisnik");
                });

            modelBuilder.Entity("Podaci.Klase.KreditnaKartica", b =>
                {
                    b.Property<int>("KreditnaKarticaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumIsteka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImeVlasnikaKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAktivna")
                        .HasColumnType("bit");

                    b.Property<string>("KupacId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VerifikacijskiKod")
                        .HasColumnType("int");

                    b.HasKey("KreditnaKarticaID");

                    b.HasIndex("KupacId");

                    b.ToTable("KreditnaKartica");
                });

            modelBuilder.Entity("Podaci.Klase.Linija", b =>
                {
                    b.Property<int>("LinijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cetvrtak")
                        .HasColumnType("bit");

                    b.Property<string>("DaniUSedmici")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradDolaskaGradID")
                        .HasColumnType("int");

                    b.Property<int>("GradPolaskaGradID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Nedjelja")
                        .HasColumnType("bit");

                    b.Property<string>("OznakaLinije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Petak")
                        .HasColumnType("bit");

                    b.Property<bool>("Ponedjeljak")
                        .HasColumnType("bit");

                    b.Property<bool>("Srijeda")
                        .HasColumnType("bit");

                    b.Property<bool>("Subota")
                        .HasColumnType("bit");

                    b.Property<bool>("Utorak")
                        .HasColumnType("bit");

                    b.Property<string>("VrijemeDolaska")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VrijemePolaska")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LinijaID");

                    b.HasIndex("GradDolaskaGradID");

                    b.HasIndex("GradPolaskaGradID");

                    b.ToTable("Linija");
                });

            modelBuilder.Entity("Podaci.Klase.LinijaVozac", b =>
                {
                    b.Property<int>("LinijaID")
                        .HasColumnType("int");

                    b.Property<int>("VozacID")
                        .HasColumnType("int");

                    b.HasKey("LinijaID", "VozacID");

                    b.HasIndex("VozacID");

                    b.ToTable("LinijaVozac");
                });

            modelBuilder.Entity("Podaci.Klase.LinijaVozilo", b =>
                {
                    b.Property<int>("LinijaID")
                        .HasColumnType("int");

                    b.Property<int>("VoziloID")
                        .HasColumnType("int");

                    b.HasKey("LinijaID", "VoziloID");

                    b.HasIndex("VoziloID");

                    b.ToTable("LinijaVozilo");
                });

            modelBuilder.Entity("Podaci.Klase.Obavijest", b =>
                {
                    b.Property<int>("ObavijestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DatumObjave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naslov")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObavijestKategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Podnaslov")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ObavijestID");

                    b.HasIndex("ObavijestKategorijaID");

                    b.ToTable("Obavijest");
                });

            modelBuilder.Entity("Podaci.Klase.Stajalista", b =>
                {
                    b.Property<int>("StajalistaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GradID")
                        .HasColumnType("int");

                    b.Property<int>("LinijaID")
                        .HasColumnType("int");

                    b.Property<int>("RedniBrojStajalista")
                        .HasColumnType("int");

                    b.Property<string>("SatnicaStizanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StajalistaID");

                    b.HasIndex("GradID");

                    b.HasIndex("LinijaID");

                    b.ToTable("Stajalista");
                });

            modelBuilder.Entity("Podaci.Klase.TipKarte", b =>
                {
                    b.Property<int>("TipKarteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAktivan")
                        .HasColumnType("bit");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipKarteID");

                    b.ToTable("TipKarte");
                });

            modelBuilder.Entity("Podaci.Klase.Vozac", b =>
                {
                    b.Property<int>("VozacID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojVozacke")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumRodjenja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatumZaposlenja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VozacID");

                    b.ToTable("Vozac");
                });

            modelBuilder.Entity("Podaci.Klase.Vozilo", b =>
                {
                    b.Property<int>("VoziloID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DatumZadnjegServisa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxBrojSjedista")
                        .HasColumnType("int");

                    b.Property<string>("OznakaVozila")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistracijskiBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoziloID");

                    b.ToTable("Vozilo");
                });

            modelBuilder.Entity("Podaci.Klase.VrstaPopusta", b =>
                {
                    b.Property<int>("VrstaPopustaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAktivan")
                        .HasColumnType("bit");

                    b.Property<float>("Iznos")
                        .HasColumnType("real");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VrstaPopustaID");

                    b.ToTable("VrstaPopusta");
                });

            modelBuilder.Entity("WebApplication1.Drzava", b =>
                {
                    b.Property<int>("DrzavaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DrzavaID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("WebApplication1.ObavijestKategorija", b =>
                {
                    b.Property<int>("ObavijestKategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObavijestKategorijaID");

                    b.ToTable("ObavijestKategorija");
                });

            modelBuilder.Entity("Podaci.Klase.Kupac", b =>
                {
                    b.HasBaseType("Podaci.Klase.Korisnik");

                    b.HasDiscriminator().HasValue("Kupac");
                });

            modelBuilder.Entity("Podaci.Klase.Menadzer", b =>
                {
                    b.HasBaseType("Podaci.Klase.Korisnik");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Menadzer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Podaci.Klase.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Podaci.Klase.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Podaci.Klase.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.Cijena", b =>
                {
                    b.HasOne("Podaci.Klase.Grad", "GradDolaska")
                        .WithMany()
                        .HasForeignKey("GradDolaskaGradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Grad", "GradPolaska")
                        .WithMany()
                        .HasForeignKey("GradPolaskaGradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.Feedback", b =>
                {
                    b.HasOne("Podaci.Klase.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacId");
                });

            modelBuilder.Entity("Podaci.Klase.Grad", b =>
                {
                    b.HasOne("WebApplication1.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.Karta", b =>
                {
                    b.HasOne("Podaci.Klase.Stajalista", "Dolaziste")
                        .WithMany()
                        .HasForeignKey("DolazisteID");

                    b.HasOne("Podaci.Klase.KreditnaKartica", "KKartica")
                        .WithMany()
                        .HasForeignKey("KKarticaID");

                    b.HasOne("Podaci.Klase.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacId");

                    b.HasOne("Podaci.Klase.Stajalista", "Polaziste")
                        .WithMany()
                        .HasForeignKey("PolazisteID");

                    b.HasOne("Podaci.Klase.TipKarte", "TipKarte")
                        .WithMany()
                        .HasForeignKey("TipKarteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.VrstaPopusta", "VrstaPopusta")
                        .WithMany()
                        .HasForeignKey("VrstaPopustaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.KreditnaKartica", b =>
                {
                    b.HasOne("Podaci.Klase.Kupac", "Kupac")
                        .WithMany()
                        .HasForeignKey("KupacId");
                });

            modelBuilder.Entity("Podaci.Klase.Linija", b =>
                {
                    b.HasOne("Podaci.Klase.Grad", "GradDolaska")
                        .WithMany()
                        .HasForeignKey("GradDolaskaGradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Grad", "GradPolaska")
                        .WithMany()
                        .HasForeignKey("GradPolaskaGradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.LinijaVozac", b =>
                {
                    b.HasOne("Podaci.Klase.Linija", "Linija")
                        .WithMany("Vozac")
                        .HasForeignKey("LinijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Vozac", "Vozac")
                        .WithMany("Linija")
                        .HasForeignKey("VozacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.LinijaVozilo", b =>
                {
                    b.HasOne("Podaci.Klase.Linija", "Linija")
                        .WithMany("Vozilo")
                        .HasForeignKey("LinijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Vozilo", "Vozilo")
                        .WithMany("Linija")
                        .HasForeignKey("VoziloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.Obavijest", b =>
                {
                    b.HasOne("WebApplication1.ObavijestKategorija", "ObavijestKategorija")
                        .WithMany()
                        .HasForeignKey("ObavijestKategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podaci.Klase.Stajalista", b =>
                {
                    b.HasOne("Podaci.Klase.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Klase.Linija", "Linija")
                        .WithMany()
                        .HasForeignKey("LinijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
