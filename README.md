# 📚 Responsi 2 Junior Project - Aplikasi Manajemen Developer

| No. | Nama | NIM |
|-----|----- |-----|
|1.   | Rhizal Maulana Khafifi | 514719 |

---

## 📋 Worksheet
Worksheet untuk repositori ini dapat diakses [di sini](https://drive.google.com/drive/folders/1dLwMnlWG3XBTjSNLorek_QtoGE6tighL)

---

## 🖥️ Tampilan Aplikasi

### 1. Interface Aplikasi di Visual Studio (Design View)
*Tampilan form design dengan kontrol DataGridView, TextBox, ComboBox, dan Button*

![Form Designer](./assets/interface-vs.png)

### 2. Hasil Build Aplikasi
*Tampilan aplikasi saat dijalankan dengan data developer*

![Aplikasi Running](./assets/interface-compiled.png)

### 3. Tombol Insert - Menambah Data Developer Baru
*Kode dan implementasi tombol Insert untuk menambahkan developer*

![Tombol Insert](./assets/button-insert.png)

### 4. Tombol Edit - Mengubah Data Developer
*Kode dan implementasi tombol Edit untuk memperbarui data developer*

![Tombol Edit](./assets/button-edit.png)

### 5. Tombol Delete - Menghapus Data Developer
*Kode dan implementasi tombol Delete untuk menghapus data developer*

![Tombol Delete](./assets/button-delete.png)

---

## ✅ Tugas Opsional

### 1. 📊 Entity Relationship Diagram (ERD)
Tabel `proyek` (Parent) dengan relasi ke tabel `developer` (Child).

![ERD](./assets/ERD.png)

---

### 2. 🗄️ Struktur Database PostgreSQL

**Tabel Proyek (Parent)**
```sql
CREATE TABLE proyek (
    id_proyek SERIAL PRIMARY KEY,
    nama_proyek VARCHAR(100),
    client VARCHAR(50),
    budget NUMERIC(15, 2)
);
```

**Tabel Developer (Child)**
```sql
CREATE TABLE developer (
    id_dev SERIAL PRIMARY KEY,
    nama_dev VARCHAR(50),
    status_kontrak VARCHAR(20),   -- 'Freelance' atau 'Full Time'
    fitur_selesai INT DEFAULT 0,
    jumlah_bug INT DEFAULT 0,
    id_proyek INTEGER REFERENCES proyek(id_proyek) ON DELETE CASCADE
);
```

**Insert Data Awal**
```sql
-- Insert Proyek
INSERT INTO proyek (nama_proyek, client, budget) VALUES 
('Aplikasi E-Commerce', 'PT Maju Jaya', 150000000),
('Sistem Absensi AI', 'Univ Teknologi', 85000000),
('Company Profile Web', 'CV Sejahtera', 25000000);

-- Insert Developer
INSERT INTO developer (nama_dev, status_kontrak, fitur_selesai, jumlah_bug, id_proyek) VALUES 
('Andi Backend', 'Full Time', 12, 2, 1),
('Budi Frontend', 'Freelance', 8, 5, 1),
('Citra Mobile', 'Full Time', 15, 0, 2),
('Dewi QA', 'Freelance', 0, 10, 1),
('Eko Fullstack', 'Full Time', 5, 1, 3);
```

---

### 3. 🎯 Menerapkan OOP (Object-Oriented Programming)

#### a. 🔒 ENCAPSULATION
Menyembunyikan data dengan `private` field, akses melalui `public` property.

**Lokasi:** `Models/Developer.cs`, `Models/Proyek.cs`, `Models/BaseModel.cs`
```csharp
public class Developer : BaseModel
{
    // Private fields
    private string _namaDeveloper = string.Empty;
    private int _fiturSelesai;
    private double _skorTotal;
    
    // Public properties dengan validasi
    public string NamaDeveloper
    {
        get { return _namaDeveloper; }
        set { _namaDeveloper = value ?? string.Empty; }
    }

    public int FiturSelesai
    {
        get { return _fiturSelesai; }
        set { _fiturSelesai = value >= 0 ? value : 0; }  // Validasi nilai negatif
    }

    public double SkorTotal
    {
        get { return _skorTotal; }
        private set { _skorTotal = value; }  // Read-only dari luar class
    }
}
```

#### b. 👪 INHERITANCE
Child class mewarisi properties dan methods dari parent class.

**Lokasi:** `Models/BaseModel.cs` → `Models/Developer.cs`, `Models/Proyek.cs`
```csharp
// Parent - Abstract class
public abstract class BaseModel
{
    private int _id;
    public int Id { get => _id; set => _id = value; }
    
    // Abstract method - wajib diimplementasikan oleh child
    public abstract bool IsValid();
}

// Child - Developer mewarisi BaseModel
public class Developer : BaseModel
{
    // Mewarisi property Id dari BaseModel
    // Wajib override method IsValid()
    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(NamaDeveloper) &&
               !string.IsNullOrWhiteSpace(NamaProyek) &&
               !string.IsNullOrWhiteSpace(StatusKontrak);
    }
}

// Child - Proyek mewarisi BaseModel
public class Proyek : BaseModel
{
    public override bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(NamaProyek);
    }
}
```

#### c. 🎭 POLYMORPHISM
Method sama (`IsValid()`), implementasi berbeda di tiap class.

**Lokasi:** Override method `IsValid()` di `Developer.cs` dan `Proyek.cs`
```csharp
// Developer - validasi 3 field
public override bool IsValid()
{
    return !string.IsNullOrWhiteSpace(NamaDeveloper) &&
           !string.IsNullOrWhiteSpace(NamaProyek) &&
           !string.IsNullOrWhiteSpace(StatusKontrak);
}

// Proyek - validasi 1 field saja
public override bool IsValid()
{
    return !string.IsNullOrWhiteSpace(NamaProyek);
}
```

#### d. 🎨 ABSTRACTION
Menyembunyikan detail implementasi SQL, user hanya panggil method CRUD.

**Lokasi:** `Repositories/IDatabaseRepository.cs` (Interface) dan `Repositories/DatabaseRepository.cs` (Implementation)
```csharp
// Interface - kontrak method yang jelas
public interface IDatabaseRepository
{
    bool Insert(Developer developer);
    List<Developer> GetAll();
    Developer? GetById(int id);
    List<Proyek> GetAllProyek();
    bool Update(Developer developer);
    bool Delete(int id);
}

// Implementation - detail SQL tersembunyi
public class DatabaseRepository : IDatabaseRepository
{
    public List<Developer> GetAll()
    {
        // User hanya panggil GetAll(), tidak perlu tahu detail SQL
        string query = @"SELECT d.id_dev, d.nama_dev, p.nama_proyek, 
                        d.status_kontrak, d.fitur_selesai, d.jumlah_bug 
                        FROM developer d 
                        JOIN proyek p ON d.id_proyek = p.id_proyek 
                        ORDER BY d.id_dev";
        // ... implementasi tersembunyi
    }
}
```

**Ringkasan OOP:**
| Pilar OOP | Lokasi | Contoh |
|-----------|--------|--------|
| **Encapsulation** | `Models/Developer.cs` | `private string _namaDeveloper;` + `public string NamaDeveloper { get; set; }` |
| **Inheritance** | `Models/` | `class Developer : BaseModel`, `class Proyek : BaseModel` |
| **Polymorphism** | `Models/` | Override `IsValid()` berbeda di Developer dan Proyek |
| **Abstraction** | `Repositories/` | Interface `IDatabaseRepository` + implementasi `DatabaseRepository` |

---

### 4. 💰 Rumus Perhitungan Skor dan Gaji

Skor dan gaji developer dihitung otomatis di aplikasi (bukan disimpan di database).

**Lokasi:** `Models/Developer.cs` - method `HitungSkorDanGaji()`

```csharp
public void HitungSkorDanGaji()
{
    // Hitung Skor Total
    // Rumus: (Fitur Selesai × 10) - (Jumlah Bug × 5)
    _skorTotal = (FiturSelesai * 10) - (JumlahBug * 5);
    if (_skorTotal < 0) _skorTotal = 0;

    // Gaji Dasar berdasarkan Status Kontrak
    decimal gajiDasar = StatusKontrak switch
    {
        "Full Time" => 5000000m,
        "Freelance" => 3000000m,
        _ => 2000000m
    };

    // Bonus berdasarkan Skor
    decimal bonus = (decimal)_skorTotal * 100000m;

    // Total Gaji = Gaji Dasar + Bonus
    _totalGaji = gajiDasar + bonus;
}
```

**Contoh Perhitungan:**
| Developer | Status | Fitur | Bug | Skor | Gaji Dasar | Bonus | Total Gaji |
|-----------|--------|-------|-----|------|------------|-------|------------|
| Andi Backend | Full Time | 12 | 2 | (12×10)-(2×5) = 110 | Rp 5.000.000 | Rp 11.000.000 | Rp 16.000.000 |
| Budi Frontend | Freelance | 8 | 5 | (8×10)-(5×5) = 55 | Rp 3.000.000 | Rp 5.500.000 | Rp 8.500.000 |
| Citra Mobile | Full Time | 15 | 0 | (15×10)-(0×5) = 150 | Rp 5.000.000 | Rp 15.000.000 | Rp 20.000.000 |

---

## 📁 Struktur Project

```
Responsi2-Junpro/
├── Models/                         # Entity classes (Encapsulation, Inheritance, Polymorphism)
│   ├── BaseModel.cs                # Abstract parent class dengan method IsValid()
│   ├── Developer.cs                # Model developer + perhitungan skor/gaji
│   └── Proyek.cs                   # Model proyek untuk dropdown
├── Repositories/                   # Repository pattern (Abstraction)
│   ├── IDatabaseRepository.cs      # Interface kontrak CRUD
│   ├── DatabaseRepository.cs       # Implementasi PostgreSQL dengan Npgsql
│   └── DatabaseConnection.cs       # Static class untuk koneksi database
├── DropDownMenu.cs                 # Helper class untuk mengelola ComboBox
├── Form1.cs                        # Windows Forms UI + Event Handlers CRUD
├── Form1.Designer.cs               # Auto-generated UI components
└── Program.cs                      # Entry point aplikasi
```

---

## 🚀 Cara Menjalankan

### 1. Setup Database PostgreSQL
```sql
CREATE DATABASE responsi;
```

### 2. Buat Tabel
```sql
-- Tabel Proyek (Parent)
CREATE TABLE proyek (
    id_proyek SERIAL PRIMARY KEY,
    nama_proyek VARCHAR(100),
    client VARCHAR(50),
    budget NUMERIC(15, 2)
);

-- Tabel Developer (Child)
CREATE TABLE developer (
    id_dev SERIAL PRIMARY KEY,
    nama_dev VARCHAR(50),
    status_kontrak VARCHAR(20),
    fitur_selesai INT DEFAULT 0,
    jumlah_bug INT DEFAULT 0,
    id_proyek INTEGER REFERENCES proyek(id_proyek) ON DELETE CASCADE
);
```

### 3. Insert Data Awal
```sql
INSERT INTO proyek (nama_proyek, client, budget) VALUES 
('Aplikasi E-Commerce', 'PT Maju Jaya', 150000000),
('Sistem Absensi AI', 'Univ Teknologi', 85000000),
('Company Profile Web', 'CV Sejahtera', 25000000);

INSERT INTO developer (nama_dev, status_kontrak, fitur_selesai, jumlah_bug, id_proyek) VALUES 
('Andi Backend', 'Full Time', 12, 2, 1),
('Budi Frontend', 'Freelance', 8, 5, 1),
('Citra Mobile', 'Full Time', 15, 0, 2);
```

### 4. Update Connection String
Edit `Repositories/DatabaseConnection.cs`:
```csharp
"Host=localhost;Port=5432;Database=responsi;Username=postgres;Password=YOUR_PASSWORD"
```

### 5. Build & Run
```bash
cd Responsi2-Junpro
dotnet build
dotnet run
```

---

## 🔧 Teknologi yang Digunakan

- **Framework:** .NET 8.0 Windows Forms
- **Database:** PostgreSQL
- **Library:** Npgsql (PostgreSQL driver for .NET)
- **Pattern:** Repository Pattern, OOP Principles (Encapsulation, Inheritance, Polymorphism, Abstraction)