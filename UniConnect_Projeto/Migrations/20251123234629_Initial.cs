using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniConnect_Projeto.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CursosTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    Coordenador = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaTable_CursosTable_CursoId",
                        column: x => x.CursoId,
                        principalTable: "CursosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Termo = table.Column<int>(type: "int", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmaTable_CursosTable_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "CursosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: true),
                    CursoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosTable_CursosTable_CursoId",
                        column: x => x.CursoId,
                        principalTable: "CursosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoEstudoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublico = table.Column<bool>(type: "bit", nullable: false),
                    CriadorId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoEstudoTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoEstudoTable_DisciplinaTable_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "DisciplinaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GrupoEstudoTable_UsuariosTable_CriadorId",
                        column: x => x.CriadorId,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecomendacaoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendacaoTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecomendacaoTable_UsuariosTable_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaAlunoTable",
                columns: table => new
                {
                    IdTurma = table.Column<int>(type: "int", nullable: false),
                    IdAluno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAlunoTable", x => new { x.IdTurma, x.IdAluno });
                    table.ForeignKey(
                        name: "FK_TurmaAlunoTable_TurmaTable_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "TurmaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaAlunoTable_UsuariosTable_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TurmaProfessorTable",
                columns: table => new
                {
                    IdTurma = table.Column<int>(type: "int", nullable: false),
                    IdProfessor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaProfessorTable", x => new { x.IdTurma, x.IdProfessor });
                    table.ForeignKey(
                        name: "FK_TurmaProfessorTable_TurmaTable_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "TurmaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaProfessorTable_UsuariosTable_IdProfessor",
                        column: x => x.IdProfessor,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoEstudoAlunoTable",
                columns: table => new
                {
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    IsModerador = table.Column<bool>(type: "bit", nullable: false),
                    EntrouEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoEstudoAlunoTable", x => new { x.GrupoId, x.AlunoId });
                    table.ForeignKey(
                        name: "FK_GrupoEstudoAlunoTable_GrupoEstudoTable_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "GrupoEstudoTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoEstudoAlunoTable_UsuariosTable_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: true),
                    GrupoId = table.Column<int>(type: "int", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTable_GrupoEstudoTable_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "GrupoEstudoTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MaterialTable_TurmaTable_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "TurmaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MaterialTable_UsuariosTable_AutorId",
                        column: x => x.AutorId,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLikeTable",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLikeTable", x => new { x.MaterialId, x.AlunoId });
                    table.ForeignKey(
                        name: "FK_MaterialLikeTable_MaterialTable_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MaterialTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialLikeTable_UsuariosTable_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "UsuariosTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaTable_CursoId",
                table: "DisciplinaTable",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoEstudoAlunoTable_AlunoId",
                table: "GrupoEstudoAlunoTable",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoEstudoTable_CriadorId",
                table: "GrupoEstudoTable",
                column: "CriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoEstudoTable_DisciplinaId",
                table: "GrupoEstudoTable",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLikeTable_AlunoId",
                table: "MaterialLikeTable",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTable_AutorId",
                table: "MaterialTable",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTable_GrupoId",
                table: "MaterialTable",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTable_TurmaId",
                table: "MaterialTable",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacaoTable_AlunoId",
                table: "RecomendacaoTable",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAlunoTable_IdAluno",
                table: "TurmaAlunoTable",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaProfessorTable_IdProfessor",
                table: "TurmaProfessorTable",
                column: "IdProfessor");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaTable_IdCurso",
                table: "TurmaTable",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosTable_CursoId",
                table: "UsuariosTable",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoEstudoAlunoTable");

            migrationBuilder.DropTable(
                name: "MaterialLikeTable");

            migrationBuilder.DropTable(
                name: "RecomendacaoTable");

            migrationBuilder.DropTable(
                name: "TurmaAlunoTable");

            migrationBuilder.DropTable(
                name: "TurmaProfessorTable");

            migrationBuilder.DropTable(
                name: "MaterialTable");

            migrationBuilder.DropTable(
                name: "GrupoEstudoTable");

            migrationBuilder.DropTable(
                name: "TurmaTable");

            migrationBuilder.DropTable(
                name: "DisciplinaTable");

            migrationBuilder.DropTable(
                name: "UsuariosTable");

            migrationBuilder.DropTable(
                name: "CursosTable");
        }
    }
}
