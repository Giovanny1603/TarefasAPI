"use client";

import { useEffect, useState } from "react";

export default function Page() {
  const [tarefas, setTarefas] = useState([]);
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [editandoId, setEditandoId] = useState(null);

  const carregarTarefas = () => {
    fetch("http://localhost:5039/api/tarefas/list")
      .then((res) => res.json())
      .then((data) => setTarefas(data))
      .catch((err) => console.error("Erro ao buscar dados:", err));
  };

  useEffect(() => {
    carregarTarefas();
  }, []);

  const criarOuAtualizarTarefa = (e) => {
    e.preventDefault();

    const tarefa = {
      titulo,
      descricao,
      concluida: false,
    };

    if (editandoId) {
      // Atualiza
      fetch(`http://localhost:5039/api/tarefas/${editandoId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ...tarefa, id: editandoId }),
      })
        .then((res) => {
          if (!res.ok) throw new Error("Erro ao atualizar tarefa");
          return res.json();
        })
        .then(() => {
          setTitulo("");
          setDescricao("");
          setEditandoId(null);
          carregarTarefas();
        })
        .catch((err) => console.error("Erro ao atualizar:", err));
    } else {
      // Cria
      fetch("http://localhost:5039/api/tarefas", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(tarefa),
      })
        .then((res) => {
          if (!res.ok) throw new Error("Erro ao criar tarefa");
          return res.json();
        })
        .then(() => {
          setTitulo("");
          setDescricao("");
          carregarTarefas();
        })
        .catch((err) => console.error("Erro ao criar tarefa:", err));
    }
  };

  const deletarTarefa = (id) => {
    fetch(`http://localhost:5039/api/tarefas/${id}`, { method: "DELETE" })
      .then((res) => {
        if (!res.ok) throw new Error("Erro ao deletar tarefa");
        return res.text();
      })
      .then(() => carregarTarefas())
      .catch((err) => console.error("Erro ao deletar tarefa:", err));
  };

  const prepararEdicao = (tarefa) => {
    setTitulo(tarefa.titulo);
    setDescricao(tarefa.descricao);
    setEditandoId(tarefa.id);
  };

  const alternarConcluida = (tarefa) => {
    const tarefaAtualizada = {
      ...tarefa,
      concluida: !tarefa.concluida,
    };

    fetch(`http://localhost:5039/api/tarefas/${tarefa.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(tarefaAtualizada),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Erro ao atualizar status");
        return res.json();
      })
      .then(() => carregarTarefas())
      .catch((err) => console.error("Erro ao alternar status:", err));
  };

  return (
    <div style={{ padding: "2rem", fontFamily: "Arial, sans-serif" }}>
      <h1>Lista de Tarefas</h1>

      <form onSubmit={criarOuAtualizarTarefa} style={{ marginBottom: "2rem" }}>
        <input
          type="text"
          placeholder="Título"
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
          required
          style={{ marginRight: "1rem", padding: "0.5rem" }}
        />
        <input
          type="text"
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
          style={{ marginRight: "1rem", padding: "0.5rem" }}
        />
        <button type="submit" style={{ padding: "0.5rem 1rem" }}>
          {editandoId ? "Atualizar" : "Criar"} Tarefa
        </button>
      </form>

      <ul>
        {tarefas.map((tarefa) => (
          <li key={tarefa.id} style={{ marginBottom: "1rem" }}>
            <strong>ID:</strong> {tarefa.id} | <strong>Título:</strong>{" "}
            {tarefa.titulo} {tarefa.concluida ? "✅" : ""}
            <br />
            <strong>Descrição:</strong> {tarefa.descricao}
            <br />
            <button onClick={() => deletarTarefa(tarefa.id)}>🗑️ Deletar</button>
            <button onClick={() => prepararEdicao(tarefa)} style={{ marginLeft: "1rem" }}>
              ✏️ Editar
            </button>
            <button onClick={() => alternarConcluida(tarefa)} style={{ marginLeft: "1rem" }}>
              {tarefa.concluida ? "Desmarcar" : "Concluir"}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}