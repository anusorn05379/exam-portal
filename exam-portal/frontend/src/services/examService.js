import { api } from './api';

export const getQuestions = async () => {
    console.log('[examService] getQuestions called');
    const response = await api.get('/api/Questions');
    console.log('[examService] getQuestions response:', response.data);
    return response.data;
}

export const submitExam = async (payload) => {
    console.log('[examService] submitExam payload:', payload);
    const response = await api.post('/api/Exams/submit', payload);
    console.log('[examService] submitExam response:', response.data);
    return response.data;
}

export const getExamResult = async (Id) => {
    console.log('[examService] getExamResult called with Id:', Id);
    const response = await api.get(`/api/Exams/${Id}`)
    console.log('[examService] getExamResult response:', response.data);
    return response.data;
} 

export const getExams = async () => {
    console.log('[examService] getExams called');
    const response = await api.get('/api/Exams');
    console.log('[examService] getExams response:', response.data);
    return response.data;
}

export default api;