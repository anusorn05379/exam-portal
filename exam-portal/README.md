# Exam Portal — IT 10-1

ระบบทำข้อสอบออนไลน์ สำหรับวิชา IT 10-1
พัฒนาด้วย Vue 3 (Frontend) + ASP.NET Core 8 (Backend) + SQLite

---

## Tech Stack

| ฝั่ง | เทคโนโลยี |
|------|-----------|
| Frontend | Vue 3, Vite, Pinia, Vue Router, Axios |
| Backend | ASP.NET Core 8, Entity Framework Core 8 |
| Database | SQLite |
| API Docs | Swagger / OpenAPI |

---

## โครงสร้างโปรเจค

```
exam-portal/
├── backend/
│   └── src/
│       └── ExamPortal.Api/       # ASP.NET Core Web API
│           ├── Program.cs
│           ├── appsettings.json
│           └── ExamPortal.Api.csproj
└── frontend/
    ├── src/
    │   ├── components/           # CandidateForm, QuestionCard, ResultSummary
    │   ├── views/                # ExamPage, ResultPage, ScorePage, HistoryPage
    │   ├── stores/               # Pinia store (exam.js)
    │   ├── services/             # API service layer
    │   └── router/               # Vue Router
    ├── index.html
    └── vite.config.js
```

---

## การติดตั้งและรันโปรเจค

### สิ่งที่ต้องมีก่อน

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)

---

### Backend

```bash
cd backend/src/ExamPortal.Api

# รัน (migrations และ seed data จะทำงานอัตโนมัติ)
dotnet run
```

API จะรันที่ `http://localhost:5001`
Swagger UI: `http://localhost:5001/swagger`

---

### Frontend

```bash
cd frontend

# ติดตั้ง dependencies
npm install

# รัน dev server
npm run dev
```

Frontend จะรันที่ `http://localhost:5173`

#### Environment Variables

สร้างไฟล์ `.env` ในโฟลเดอร์ `frontend/`:

```env
VITE_API_BASE_URL=http://localhost:5001
```

---

## API Endpoints

| Method | Endpoint | คำอธิบาย |
|--------|----------|----------|
| GET | `/api/Questions` | ดึงรายการคำถามทั้งหมด |
| POST | `/api/Exams/submit` | ส่งคำตอบและรับผลสอบ |
| GET | `/api/Exams/{id}` | ดูผลสอบตาม ID |
| GET | `/api/Exams` | ดูประวัติการสอบทั้งหมด |

---

## หน้าจอหลัก

| Path | หน้า |
|------|------|
| `/` | หน้าทำข้อสอบ (กรอกชื่อ + ตอบคำถาม) |
| `/result` | หน้าแสดงผลหลังส่งคำตอบ |
| `/score/:id` | หน้าดูรายละเอียดผลสอบ |
| `/history` | หน้าประวัติการสอบทั้งหมด |
