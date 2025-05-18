'use client';

import { useEffect, useState } from 'react';

export default function Home() {
  const API_URL = 'http://localhost:5039';
  const [tarefas, setTarefas] = useState([]);
  const [titulo, setTitulo] = useState('');
  const [descricao, setDescricao] = useState('');
  const [editando, setEditando] = useState(null);

  const fetchTarefas = async () => {
    const res = await fetch(`${API_URL}/tarefas`);
    const data = await res.json();
    setTarefas(data);
  };

  useEffect(() => {
    fetchTarefas();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const tarefa = { titulo, descricao, concluida: false };

    if (editando) {
      await fetch(`${API_URL}/tarefas/${editando}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(tarefa),
      });
      setEditando(null);
    } else {
      await fetch(`${API_URL}/tarefas`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(tarefa),
      });
    }

    setTitulo('');
    setDescricao('');
    fetchTarefas();
  };

  const handleDelete = async (id) => {
    await fetch(`${API_URL}/tarefas/${id}`, {
      method: 'DELETE',
    });
    fetchTarefas();
  };

  const handleEdit = (tarefa) => {
    setTitulo(tarefa.titulo);
    setDescricao(tarefa.descricao);
    setEditando(tarefa.id);
  };

  const toggleConcluida = async (tarefa) => {
    await fetch(`${API_URL}/tarefas/${tarefa.id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...tarefa, concluida: !tarefa.concluida }),
    });
    fetchTarefas();
  };

  return (
    <div style={{ padding: '2rem', fontFamily: 'Arial' }}>
      <h1>Quadro de Tarefas</h1>
      <form onSubmit={handleSubmit} style={{ marginBottom: '2rem' }}>
        <input
          type="text"
          placeholder="Título"
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
          required
          style={{ marginRight: '0.5rem' }}
        />
        <input
          type="text"
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
          style={{ marginRight: '0.5rem' }}
        />
        <button type="submit">{editando ? 'Atualizar' : 'Criar'}</button>
      </form>

      <div style={{ display: 'flex', gap: '2rem' }}>
        <div>
          <h2>Não concluídas</h2>
          {tarefas.filter(t => !t.concluida).map((tarefa) => (
            <div key={tarefa.id} style={cardStyle}>
              <h3>{tarefa.titulo}</h3>
              <p>{tarefa.descricao}</p>
              <button onClick={() => toggleConcluida(tarefa)}>Concluir</button>
              <button onClick={() => handleEdit(tarefa)}>Editar</button>
              <button onClick={() => handleDelete(tarefa.id)}>Deletar</button>
            </div>
          ))}
        </div>

        <div>
          <h2>Concluídas</h2>
          {tarefas.filter(t => t.concluida).map((tarefa) => (
            <div key={tarefa.id} style={{ ...cardStyle, backgroundColor: '#d3f9d8' }}>
              <h3>{tarefa.titulo}</h3>
              <p>{tarefa.descricao}</p>
              <button onClick={() => toggleConcluida(tarefa)}>Desmarcar</button>
              <button onClick={() => handleDelete(tarefa.id)}>Deletar</button>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

const cardStyle = {
  border: '1px solid #ccc',
  borderRadius: '8px',
  padding: '1rem',
  marginBottom: '1rem',
  backgroundColor: '#f1f1f1',
};
