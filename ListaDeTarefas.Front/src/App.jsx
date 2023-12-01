import "./App.css";
import { MdDone } from "react-icons/md";
import { FaTrashAlt } from "react-icons/fa";
import { useEffect, useState, useRef } from "react";
import { IoSearchOutline } from "react-icons/io5";
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Header from "./components/HeaderComponent/Header";

function App() {
  const [data, setData] = useState([]);
  const [taskName, setTaskName] = useState("");
  const [filteredData, setFilteredData] = useState([]);
  const [taskFilterValue, setTaskFilterValue] = useState("");
  const [isDisabled, setIsDisabled] = useState(true);
  const filterSpanRef = useRef(null);
  const filterInputRef = useRef(null);
  const filterContainerRef = useRef(null);
  const urlAPI = "https://localhost:7276/api/Task";

  const notify = () =>
    toast.success("Tarefa adicionada!", {
      position: "top-right",
      autoClose: 1500,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });
  const notifyError = () =>
    toast.error("Tarefa já existente!", {
      position: "top-right",
      autoClose: 1500,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });
  const notifyDelete = () =>
    toast.success("Tarefa removida!", {
      position: "top-right",
      autoClose: 1500,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });

  useEffect(() => {
    async function getData() {
      try {
        const response = await axios.get(urlAPI);
        setData(response.data);
      } catch (error) {
        console.error("Erro ao buscar tarefas: ", error);
      }
    }

    getData();
  }, []);

  useEffect(() => {
    taskName.length > 0 ? setIsDisabled(false) : setIsDisabled(true);
  }, [taskName]);

  const handleKeyDown = (e) => {
    if (e.keyCode === 13) {
      handleAddTask();
    }
  };

  const handleAddTask = async () => {
    try {
      await axios.post(`${urlAPI}`, {
        name: taskName,
        isCompleted: false,
      });

      const response = await axios.get(urlAPI);
      setData(response.data);

      setTaskName("");
    } catch (error) {
      notifyError();
      return console.log("Error while adding item:", error);
    }
    notify();
  };

  const handleDeleteItem = async (id) => {
    try {
      await axios.delete(`${urlAPI}/${id}`);
      const updatedTasks = await axios.get(urlAPI);
      setData(updatedTasks.data);
    } catch (error) {
      return console.error("Error trying to delete item:", error);
    }
    notifyDelete();
  };

  const handleChangeCompleted = async (id) => {
    const taskToUpdate = data.find((item) => item.id == id);

    if (!taskToUpdate) return;

    const updatedTask = {
      ...taskToUpdate,
      isCompleted: !taskToUpdate.isCompleted,
    };

    try {
      await axios.put(`${urlAPI}/${id}`, updatedTask);
      const response = await axios.get(urlAPI);
      setData(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleEnableFilter = () => {
    filterSpanRef.current.classList.add("disabled");
    filterInputRef.current.classList.remove("disabled");
    filterContainerRef.current.classList.add("wide");
  };

  const handleGenerateExcel = async () => {
    try {
      const response = await axios.get(`${urlAPI}/gerarExcel`, {
        responseType: "blob",
      });

      const url = window.URL.createObjectURL(new Blob([response.data]));

      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", "tarefas.xlsx");
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    } catch (error) {
      console.error("Erro ao gerar o arquivo Excel:", error);
    }
  };

  return (
    <>
        <Header/>
        <div className="mainContent">
          <ToastContainer />
          <main className="mainContent">
            <div className="header">
              <h1>lista de tarefas</h1>
            </div>
            <div className="inputArea">
              <input
                onKeyDown={(e) => handleKeyDown(e)}
                type="text"
                name="inputTask"
                id="inputTask"
                placeholder="Nome da tarefa"
                value={taskName}
                onChange={(e) => setTaskName(e.target.value)}
              />
              <button onClick={() => handleAddTask()} disabled={isDisabled}>
                Adicionar
              </button>
            </div>
            <div className="filterTask" ref={filterContainerRef}>
              <div className="filterIcon" onClick={() => handleEnableFilter()}>
                <span ref={filterSpanRef}>Filtrar</span>
                <input
                  ref={filterInputRef}
                  value={taskFilterValue}
                  onChange={(e) => {
                    setTaskFilterValue(e.target.value);
                    setFilteredData(
                      data.filter((item) => item.name.includes(e.target.value))
                    );
                  }}
                  type="text"
                  name="filterTask"
                  id="filterTask"
                  placeholder="Filtrar tarefa"
                  className="disabled"
                />
                <IoSearchOutline />
              </div>
            </div>
            {data.length > 0 && (
              <div className="taskInfo">
                <div className="taskStatus">
                  <p>
                    Total: <span>{data.length}</span>
                  </p>
                  <p>
                    Completas:{" "}
                    <span>
                      {data.filter((x) => x.isCompleted == true).length}
                    </span>
                  </p>
                </div>
                <div className="exportExcel">
                  <button onClick={() => handleGenerateExcel()}>
                    Gerar Planilha
                  </button>
                </div>
              </div>
            )}
            {taskFilterValue
              ? filteredData.map((itemFiltered) => (
                  <div key={itemFiltered.id} className="taskComponent">
                    <div className="taskName">
                      <input
                        type="checkbox"
                        name={itemFiltered.name}
                        id={itemFiltered.name}
                        onChange={() => {
                          handleChangeCompleted(itemFiltered.id);
                          itemFiltered.isCompleted = !itemFiltered.isCompleted;
                        }}
                        checked={itemFiltered.isCompleted}
                      />
                      <MdDone className="taskNameIcon" />
                      <label htmlFor={itemFiltered.name}>
                        {itemFiltered.name}
                      </label>
                    </div>
                    <div className="taskDelete">
                      <button
                        onClick={() => {
                          handleDeleteItem(itemFiltered.id);
                          setFilteredData(
                            data.filter((dataItem) =>
                              dataItem.name.includes(
                                taskFilterValue != itemFiltered.name
                              )
                            )
                          );
                        }}
                      >
                        <FaTrashAlt className="taskDeleteIcon" />
                      </button>
                    </div>
                  </div>
                ))
              : data.map((item) => (
                  <div key={item.id} className="taskComponent">
                    <div className="taskName">
                      <input
                        type="checkbox"
                        name={item.name}
                        id={item.name}
                        onChange={() => handleChangeCompleted(item.id)}
                        checked={item.isCompleted}
                      />
                      <MdDone className="taskNameIcon" />
                      <label htmlFor={item.name}>{item.name}</label>
                    </div>
                    <div className="taskDelete">
                      <button onClick={() => handleDeleteItem(item.id)}>
                        <FaTrashAlt className="taskDeleteIcon" />
                      </button>
                    </div>
                  </div>
                ))}
          </main>
        </div>
    </>
  );
}

export default App;
