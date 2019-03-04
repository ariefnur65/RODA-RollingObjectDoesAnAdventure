using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour {
    //Object Pooler
    public GameObject tutorialField;
    public ObjectPooler theTutorialField;
    public ObjectPooler theTutorialFieldCoin;
    public ObjectPooler theTutorialFieldPaku;
    public ObjectPooler theTutorialFieldJurang;
    public ObjectPooler theTutorialFieldPenandaJurang;
    public ObjectPooler theTutorialFieldPelapisJurangTutorial;

    //Bagian integer random
    public int tutorialFieldThreshold;
    public int tutorialFieldCoinThreshold;
    public int tutorialFieldPakuThreshold;
    public int tutorialJurangThreshold;
    public int tutorialPenandaJurangThreshold;
    public static int tutorialCount = 0;
    public string SelectField;


    public Transform generationPoint;
    public GameObject field;
    public GameObject depanField;
    public GameObject belakangField;
    public GameObject depanPenanda;
    public GameObject belakangPenanda;
    public float platformWidth;
    public float platformHeight;
    public float platformDown;
    public ObjectPooler[] theFieldPooler; //Tempat Gunung
    public ObjectPooler[] theJurangPooler; //Tempat Jurang
    public ObjectPooler theJurangDeath;
    public ObjectPooler[] thePenandaJurang;
    public ObjectPooler[] thePowerUpPooler;
    public ObjectPooler[] theFieldCoinPools;
    public ObjectPooler[] theJurangCoinPools;
    public ObjectPooler[] thePenandaJurangCoinPools;
    public ObjectPooler[] theFieldObsPools;
    public ObjectPooler[] thePenandaJurangObs;
    public ObjectPooler thePenambal;
    //Pelapis Field
    public ObjectPooler[] thePelapisFieldDepan;
    public ObjectPooler[] thePelapisFieldBelakang;
    public ObjectPooler[] thePelapisPenandaJurangDepan;
    public ObjectPooler[] thePelapisPenandaJurangBelakang;
    public ObjectPooler[] thePelapisJurang;
    public ObjectPooler[] thePelapisTerdepan;
    public ObjectPooler[] thePelapisTerdepanSebelumJurang;
    public ObjectPooler[] thePelapisLanjutanJurang;
    public ObjectPooler thePelapisSetalahJurangTerdepan;
    /*
     Algoritma pelapis field
        1. Field tengah di buat dengna posisi (x,y,z)
        2. Field belakang di buat dengan posisi (x,y,z+1)
        3. Field depan di buat dengan posisi (x,y-3,z-2)
        
         */
    //public GameObject jurangDeath; //Digunakan untuk menjadi Collider kmatian jurang
    public GameObject Camera;
    //Randomisasi Variabel
    private int randomSelector;
    private int randomJurangHappendSelector;
    private int randomJurangSelector;
    private int randomPenandaJurangObsSelector;
    private int randomPenandaJurangCoinSelector;
    private int randomPenandaJurangSelector;
    private int randomJurangCoinSelector;
    private int randomFieldCoinSelector;
    private int randomFieldObsSelector;
   

    public int PowerUpThreshold; //Thresshold kemunculan obstacle
    public int jurangCoinThreshold; //Thresshold kemunculan jurang bercoin
    public int penandaJurangCoinThreshold; //Threshold untuk field penanda jurang
    public int fieldCoinThreshold; //Threshold kemunculan field bercoin
    public int jurangObsThreshold; // Threshold munculnya Obstacle di penanda jurang
    public int fieldObsThreshold; //Threshold munculnya Obstacle di Field
    public int ThresholdJurangHappen;
    //threshold bounce
    public int BounceThreshold;
    //CoinGenerator Variable
    private CoinGenerator coinGenerator;
    private float coinThreshold;

    private TutorialSwitch theTutorialSwitch;
    private KarakterSkrip thePlayer;
    public void Start()
    {   //Mengambil Ukuran Platform
        //platformWidth = 16; //100px = 1 Unity
        //platformHeight = 10;

        
        transform.position = field.gameObject.transform.position;
        coinGenerator = FindObjectOfType<CoinGenerator>();
        // Void Start
        tutorialCount = 0;

        theTutorialSwitch = FindObjectOfType<TutorialSwitch>();
        thePlayer = FindObjectOfType<KarakterSkrip>();


    }



    //Method ujicoba Kalo udah ada Game manager nanti dihapus
    public void getStartPosition() {
        transform.position = field.gameObject.transform.position;
    }

    public void Update()
    {   //Selama titik generator dibelakang generationPoint maka instansiasi Object
        if (thePlayer.isPlaying)
        {
            if (transform.position.x < generationPoint.position.x)
            { //Do something
                randomSelector = Random.Range(0, theFieldPooler.Length); //Untuk Field
                //randomJurangSelector = Random.Range(0, theJurangPooler.Length); //Untuk Jurang
                                                                                //geser titik generator ke posisi baru
                transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y - platformHeight + platformDown, transform.position.z);

                GameObject newField;
                //Gimana supaya membuat kamera tidak turun ketika di jurang "How to"
                //====Gak jadi dipake====
                // Membuat 2 object pilar diantara 2 ujuang jurng
                //2 Object tersebut menegasikan nilai boolean "jurang" didalam player
                // Ketika jurang true maka kamera tidak mengikuti roda jika roda turun kebawah (Posisi baru - posisi lama = Negative)
                //=====Algoritma Fais========
                //Membuat Obstacle untu death di setiap tengah tengah jurang ke bawah sekitar 400px = 4 unity unit
                //saat Obstacle disentuh kamera tidak mengikuti roda kemudian obsctacle hilang memunculkan death scene
                // Kemabli ke Start Point

                /* ===Algoritma Memunculkan Koin===
                    Koin Muncul
                    dengan 3 pattern =
                    1. Lurus (4)
                    2. Melengkung (6)
                    3. Lingkaran (12)
                    Koin muncul dimana saja ? 

                    Awal setiap field
                    tengah tengah atas field
                    akhir field

                    Tengah2 atas Jurang


                    ===Pemunculan Pattern setiap field ber koin
                    Field memiliki koin 11 Field
                    Jurang memiliki koin 3 Jurang
                    Sebelum di tampilkan jurang akan dipilih antara jurang normal atau coin, begitupun dengan field

                 */

                if (theTutorialSwitch.tutorialSwitch == false)
                {

                    randomJurangHappendSelector = Random.Range(0, 100);
                    #region JurangHappen
                    if (randomJurangHappendSelector < ThresholdJurangHappen) //Jurang Muncul 20%  menentukan probabilitas dari jurang dengan 80% Gunung 20% Jurang
                    { //Penanda jurang muncul ketika jurang akan di pilih dengan menambah sebuah field seblm
                        GameObject penandaJurang;
                        GameObject pelapisTerDepan;
                        GameObject tambalField;
                        tambalField = thePenambal.GetPooledObject();
                        tambalField.transform.position = new Vector3(transform.position.x, transform.position.y - 9, transform.position.z);
                        tambalField.transform.rotation = transform.rotation;
                        tambalField.SetActive(true);

                        //Membuat pelapis terdepan Penanda
                        pelapisTerDepan = thePelapisTerdepanSebelumJurang[Random.Range(0, thePelapisTerdepanSebelumJurang.Length)].GetPooledObject();
                        pelapisTerDepan.transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
                        pelapisTerDepan.transform.rotation = transform.rotation;
                        pelapisTerDepan.SetActive(true);

                        //mulainya percabangan penanda jurang dengan obstacle dan tanpa obstacle
                        if (Random.Range(0, 100) < jurangObsThreshold) //Jika lebih kurang dari Threshold maka muncul Obs
                        { //Muncul Obs
                            randomPenandaJurangObsSelector = Random.Range(0, thePenandaJurangObs.Length);
                            penandaJurang = thePenandaJurangObs[randomPenandaJurangObsSelector].GetPooledObject(); //Random
                            penandaJurang.transform.position = transform.position;
                            penandaJurang.transform.rotation = transform.rotation;
                            penandaJurang.SetActive(true);

                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < penandaJurang.transform.childCount; i++)
                            {
                                GameObject child = penandaJurang.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }
                            UsePenandaPelapis(penandaJurang);
                        }
                        else if (Random.Range(0, 100) < penandaJurangCoinThreshold)
                        {//PenadaJurang Bercoin
                            randomPenandaJurangCoinSelector = Random.Range(0, thePenandaJurangCoinPools.Length);
                            penandaJurang = thePenandaJurangCoinPools[randomPenandaJurangCoinSelector].GetPooledObject(); //Random
                            penandaJurang.transform.position = transform.position;
                            penandaJurang.transform.rotation = transform.rotation;
                            penandaJurang.SetActive(true);
                            penandaJurang.GetComponent<PlatformCoinDestroyer>().samplingPosisiChild();

                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < penandaJurang.transform.childCount; i++)
                            {
                                GameObject child = penandaJurang.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }
                            UsePenandaPelapis(penandaJurang);

                        }
                        else
                        { //Tidak muncul = 
                            randomPenandaJurangSelector = Random.Range(0, thePenandaJurang.Length);
                            penandaJurang = thePenandaJurang[randomPenandaJurangSelector].GetPooledObject(); //Random
                            penandaJurang.transform.position = transform.position;
                            penandaJurang.transform.rotation = transform.rotation;
                            penandaJurang.SetActive(true);
                            UsePenandaPelapis(penandaJurang);

                        }


                        //CoinTampil(0, 30, new Vector3(transform.position.x - 8.5f, transform.position.y + 6.5f, transform.position.z));
                        //Generator pindah kedepan agar jurang dibuat di depan Penanda
                        transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y - platformHeight + platformDown, transform.position.z);
                        //Menampilkan pelapis bawah

                        //Mengambil Jurang dari Pooler
                        //Percabangan akan menampilkan jurang coin atau jurang biasa
                        if (Random.Range(0, 100) < jurangCoinThreshold) //Jurang muncul ketika dibawah threshold
                        {//percabangan ketika kecepatan  belum 8
                            if (thePlayer.getGayaDorongStore() > 7.5f)
                            {
                                randomJurangSelector = Random.Range(0, theJurangCoinPools.Length);
                            }
                            else
                            {
                                randomJurangSelector = Random.Range(2, theJurangCoinPools.Length); //Jurang 0 tidak bisa di lewati kalau kurang dari 7.5 makanya tidak di generate
                            }
                            newField = theJurangCoinPools[randomJurangSelector].GetPooledObject(); //Random
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);
                            //mengubah default posisi coin
                            newField.GetComponent<PlatformCoinDestroyer>().samplingPosisiChild();

                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < newField.transform.childCount; i++)
                            {
                                GameObject child = newField.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }
                            UseJurangPelapis(newField);
                            //Tempat SlowMo Muncul
                            SlowMoAppear();

                        }
                        else
                        {
                            if (thePlayer.getGayaDorongStore() > 7.5f)
                            {
                                randomJurangSelector = Random.Range(0, theJurangPooler.Length);
                            }
                            else
                            {
                                randomJurangSelector = Random.Range(1, theJurangPooler.Length); //Jurang 0 tidak bisa di lewati kalau kurang dari 7.5 makanya tidak di generate
                            }
                            newField = theJurangPooler[randomJurangSelector].GetPooledObject();
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);

                            UseJurangPelapis(newField);
                            //Tempat SlowMo Muncul
                            SlowMoAppear();
                        }
                        //CoinTampil(Random.Range(1,2), 20, new Vector3(transform.position.x-2f , transform.position.y + 6.5f, transform.position.z));

                        //Instantiate(jurangDeath, new Vector3(transform.position.x, transform.position.y - 4, transform.position.z), transform.rotation);
                        //Mengambil Death Jurang pooler
                        GameObject colliderJurang = theJurangDeath.GetPooledObject();
                        colliderJurang.transform.position = new Vector2(transform.position.x, transform.position.y - 7);
                        colliderJurang.transform.rotation = transform.rotation;
                        colliderJurang.SetActive(true);
                        #endregion
                        #region FieldSesudahJurang
                        //Percabangan field coin dan field biasa

                        transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y - platformHeight + platformDown, transform.position.z);

                        if (Random.Range(0, 100) < fieldCoinThreshold)
                        { //Coin Muncul
                            randomFieldCoinSelector = Random.Range(0, theFieldCoinPools.Length);
                            newField = theFieldCoinPools[randomFieldCoinSelector].GetPooledObject(); //Random
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);
                            newField.GetComponent<PlatformCoinDestroyer>().samplingPosisiChild();

                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < newField.transform.childCount; i++)
                            {
                                GameObject child = newField.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }
                            UsePelapisSesudahJurang(newField);



                        }
                        else if (Random.Range(0, 100) < fieldObsThreshold)
                        {
                            //Obstacle Muncul
                            randomFieldObsSelector = Random.Range(0, theFieldObsPools.Length);
                            newField = theFieldObsPools[randomFieldObsSelector].GetPooledObject(); //Random
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);
                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < newField.transform.childCount; i++)
                            {
                                GameObject child = newField.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }

                            UsePelapisSesudahJurang(newField);
                        }
                        else
                        { //Coin dan Obstacle tidak muncul

                            newField = theFieldPooler[randomSelector].GetPooledObject();
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);

                            //Pelapis Field
                            UsePelapisSesudahJurang(newField);
                        }



                        #endregion
                    }
                    #region Field Biasa
                    else //Muncul Field Gak pake jurang
                    {
                        //Percabangan field coin dan field biasa
                        if (Random.Range(0, 100) < fieldCoinThreshold)
                        { //Coin Muncul
                            randomFieldCoinSelector = Random.Range(0, theFieldCoinPools.Length);
                            newField = theFieldCoinPools[randomFieldCoinSelector].GetPooledObject(); //Random
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);
                            newField.GetComponent<PlatformCoinDestroyer>().samplingPosisiChild();

                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < newField.transform.childCount; i++)
                            {
                                GameObject child = newField.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }
                            UsePelapis(newField);



                        }
                        else if (Random.Range(0, 100) < fieldObsThreshold)
                        {
                            //Obstacle Muncul
                            randomFieldObsSelector = Random.Range(0, theFieldObsPools.Length);
                            newField = theFieldObsPools[randomFieldObsSelector].GetPooledObject(); //Random
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);
                            //Mengaktifkan Child yang ada
                            for (int i = 0; i < newField.transform.childCount; i++)
                            {
                                GameObject child = newField.transform.GetChild(i).gameObject;
                                child.SetActive(true);
                            }

                            UsePelapis(newField);
                        }
                        else
                        { //Coin dan Obstacle tidak muncul

                            newField = theFieldPooler[randomSelector].GetPooledObject();
                            newField.transform.position = transform.position;
                            newField.transform.rotation = transform.rotation;
                            newField.SetActive(true);

                            //Pelapis Field
                            UsePelapis(newField);
                        }

                        //========Randomisasi Obstacle========
                        //Jika random Kurang dari threshold maka akan muncul obstacle:
                        if (Random.Range(0, 100) < PowerUpThreshold)
                        {
                            GameObject Obstacle;
                            //menentukan Posisi X Obstacle, secara random
                            if (Random.Range(0, 100) < BounceThreshold)
                            {
                                Obstacle = thePowerUpPooler[3].GetPooledObject();

                            }
                            else
                            {
                                Obstacle = thePowerUpPooler[Random.Range(0, thePowerUpPooler.Length - 2)].GetPooledObject();

                            }
                            //Obstacle.transform.position = new Vector2 (Random.Range(transform.position.x + 8, transform.position.x), transform.position.y + 6);
                            //Menentukan X,Y berdasarkan awal dari setiap platform (Disambungan)
                            Obstacle.transform.position = new Vector2(transform.position.x - 8, transform.position.y + 7.5f);
                            Obstacle.transform.rotation = transform.rotation;
                            Obstacle.SetActive(true);
                        }
                        else
                        {
                            //CoinTampil(0, 30, new Vector3(transform.position.x - 8.5f, transform.position.y + 6.5f, transform.position.z));
                        }
                        #endregion
                    }



                    //            Instantiate(platform,transform.position,transform.rotation);
                }
                else
                {
                    //---------------------------------------TutorialFieldPooler--------------------------------------------

                    SelectField = theTutorialSwitch.SelectField;
                    if (SelectField == "Kosong")
                    {
                        Debug.Log(SelectField);
                        newField = theTutorialField.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);
                        UsePelapis(newField);
                    }
                    else if (SelectField == "Coin")
                    {
                        Debug.Log(SelectField);

                        newField = theTutorialFieldCoin.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);
                        UsePelapis(newField);

                    }
                    else if (SelectField == "Paku")
                    {
                        newField = theTutorialFieldPaku.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);
                        UsePelapis(newField);
                    }
                    else if (SelectField == "Penanda")
                    {
                        newField = theTutorialFieldPenandaJurang.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);
                        UsePenandaPelapis(newField);

                        //Penanda Jurang
                        GameObject pelapisTerdepan;
                        pelapisTerdepan = thePelapisTerdepanSebelumJurang[Random.Range(0, thePelapisTerdepanSebelumJurang.Length)].GetPooledObject();
                        pelapisTerdepan.transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                        pelapisTerdepan.transform.rotation = transform.rotation;
                        pelapisTerdepan.SetActive(true);
                    }
                    else if (SelectField == "Jurang")
                    {
                        newField = theTutorialFieldJurang.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);

                        GameObject pelapisJurang = theTutorialFieldPelapisJurangTutorial.GetPooledObject();
                        
                        GameObject pelapisLanjutan = thePelapisLanjutanJurang[6].GetPooledObject();


                        pelapisJurang.transform.position = transform.position;
                        pelapisJurang.transform.rotation = transform.rotation;
                        pelapisJurang.SetActive(true);

                        pelapisLanjutan.transform.position = new Vector3(transform.position.x, transform.position.y - 15.8f, transform.position.z);
                        pelapisLanjutan.transform.rotation = transform.rotation;
                        pelapisLanjutan.SetActive(true);



                    }
                    else if (SelectField == "Kosong2")
                    {
                        Debug.Log("Masuk gan");
                        newField = theTutorialField.GetPooledObject();
                        newField.transform.position = transform.position;
                        newField.transform.rotation = transform.rotation;
                        newField.SetActive(true);
                        UsePelapisSesudahJurang(newField);

                    }
                    tutorialCount += 1;



                    //---------------------------------------TutorialField--------------------------------------------
                }

            }   
        }
        
    }

    public void CoinTampil(int patternCoin, int ThresholdCoin, Vector3 posisiStart) //new Vector3(transform.position.x - 8.5f, transform.position.y + 6.5f, transform.position.z)
    {
        if (Random.Range(0, 100) < ThresholdCoin)
        {
            coinGenerator.SpawnCoin(patternCoin, posisiStart);
        }
    }

    private GameObject PelapisDepan(GameObject Field)
    {
        //Mendeteksi tag setiap field yang dimasukan


        switch (Field.tag)
        {
            case "Field":
                return thePelapisFieldDepan[0].GetPooledObject();

            case "Field1":
                return thePelapisFieldDepan[1].GetPooledObject();
            case "Field2":
                return thePelapisFieldDepan[2].GetPooledObject();
            case "Field3":
                return thePelapisFieldDepan[3].GetPooledObject();
            case "Field4":
                return thePelapisFieldDepan[4].GetPooledObject();
            case "Field5":
                return thePelapisFieldDepan[5].GetPooledObject();
            case "Field6":
                return thePelapisFieldDepan[6].GetPooledObject();
            case "Field7":
                return thePelapisFieldDepan[7].GetPooledObject();
            case "Field8":
                return thePelapisFieldDepan[8].GetPooledObject();
            case "Field9":
                return thePelapisFieldDepan[9].GetPooledObject();
            case "Field10":
                return thePelapisFieldDepan[10].GetPooledObject();
            case "Field11":
                return thePelapisFieldDepan[11].GetPooledObject();
            case "Field12":
                return thePelapisFieldDepan[12].GetPooledObject();
            case "Field13":
                return thePelapisFieldDepan[13].GetPooledObject();
            case "Field14":
                return thePelapisFieldDepan[14].GetPooledObject();
            case "Field15":
                return thePelapisFieldDepan[15].GetPooledObject();
            case "TutorialField":
                return thePelapisFieldDepan[16].GetPooledObject();
            default:
                return thePelapisFieldDepan[0].GetPooledObject();

        }
    }

    private GameObject PelapisBelakang(GameObject Field)
    {
        //Mendeteksi tag setiap field yang dimasukan
        switch (Field.tag)
        {
            case "Field":
                return thePelapisFieldBelakang[0].GetPooledObject();

            case "Field1":
                return thePelapisFieldBelakang[1].GetPooledObject();
            case "Field2":
                return thePelapisFieldBelakang[2].GetPooledObject();
            case "Field3":
                return thePelapisFieldBelakang[3].GetPooledObject();
            case "Field4":
                return thePelapisFieldBelakang[4].GetPooledObject();
            case "Field5":
                return thePelapisFieldBelakang[5].GetPooledObject();
            case "Field6":
                return thePelapisFieldBelakang[6].GetPooledObject();
            case "Field7":
                return thePelapisFieldBelakang[7].GetPooledObject();
            case "Field8":
                return thePelapisFieldBelakang[8].GetPooledObject();
            case "Field9":
                return thePelapisFieldBelakang[9].GetPooledObject();
            case "Field10":
                return thePelapisFieldBelakang[10].GetPooledObject();
            case "Field11":
                return thePelapisFieldBelakang[11].GetPooledObject();
            case "Field12":
                return thePelapisFieldBelakang[12].GetPooledObject();
            case "Field13":
                return thePelapisFieldBelakang[13].GetPooledObject();
            case "Field14":
                return thePelapisFieldBelakang[14].GetPooledObject();
            case "Field15":
                return thePelapisFieldBelakang[15].GetPooledObject();
            case "TutorialField":
                return thePelapisFieldBelakang[16].GetPooledObject();
            default:
                return thePelapisFieldBelakang[0].GetPooledObject();

        }
    }

    private void UsePelapis(GameObject newField)
    {
        depanField = PelapisDepan(newField);
        depanField.transform.position = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z - 1);
        depanField.transform.rotation = transform.rotation;
        depanField.SetActive(true);

        belakangField = PelapisBelakang(newField);
        belakangField.transform.position = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z + 0.75f);
        belakangField.transform.rotation = transform.rotation;
        belakangField.SetActive(true);

        UsePelapisPalingDepan(newField);
    }

    private void UsePelapisSesudahJurang(GameObject newField)
    {
        depanField = PelapisDepan(newField);
        depanField.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 1);
        depanField.transform.rotation = transform.rotation;
        depanField.SetActive(true);

        belakangField = PelapisBelakang(newField);
        belakangField.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 0.75f);
        belakangField.transform.rotation = transform.rotation;
        belakangField.SetActive(true);

        GameObject PelapisTerdepanSebelum = thePelapisSetalahJurangTerdepan.GetPooledObject();
        PelapisTerdepanSebelum.transform.position = new Vector3(transform.position.x, transform.position.y - 6, transform.position.z);
        PelapisTerdepanSebelum.transform.rotation = transform.rotation;
        PelapisTerdepanSebelum.SetActive(true);
    }
    private GameObject PelapisTandaDepan(GameObject Penanda)
    {

        switch (Penanda.tag)
        {
            case "Penanda":
                return thePelapisPenandaJurangDepan[0].GetPooledObject();
            case "Penanda1":
                return thePelapisPenandaJurangDepan[1].GetPooledObject();
            case "Penanda2":
                return thePelapisPenandaJurangDepan[2].GetPooledObject();
            default:
                return thePelapisPenandaJurangDepan[0].GetPooledObject();

        }
    }

    private GameObject PelapisTandaBelakang(GameObject Penanda)
    {

        switch (Penanda.tag)
        {
            case "Penanda":
                return thePelapisPenandaJurangBelakang[0].GetPooledObject();
            case "Penanda1":
                return thePelapisPenandaJurangBelakang[1].GetPooledObject();
            case "Penanda2":
                return thePelapisPenandaJurangBelakang[2].GetPooledObject();
            default:
                return thePelapisPenandaJurangBelakang[0].GetPooledObject();

        }
    }

    private void UsePenandaPelapis(GameObject newField)
    {
        depanField = PelapisTandaDepan(newField);
        depanField.transform.position = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z - 1);
        depanField.transform.rotation = transform.rotation;
        depanField.SetActive(true);

        belakangField = PelapisTandaBelakang(newField);
        belakangField.transform.position = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z + 0.75f);
        belakangField.transform.rotation = transform.rotation;
        belakangField.SetActive(true);
    }

    private void UseJurangPelapis(GameObject Jurang)
    {
        GameObject pelapis;
        GameObject pelapisLanjutan; 
        switch (Jurang.tag)
        {
            case "Jurang":
                pelapis = thePelapisJurang[0].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[0].GetPooledObject();
                break;

            case "Jurang1":
                pelapis = thePelapisJurang[1].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[1].GetPooledObject();
                break;

            case "Jurang2":
                pelapis = thePelapisJurang[2].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[2].GetPooledObject();
                break;

            case "Jurang3":
                pelapis = thePelapisJurang[3].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[3].GetPooledObject();
                break;

            case "Jurang4":
                pelapis = thePelapisJurang[4].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[4].GetPooledObject();
                break;

            case "Jurang5":
                pelapis = thePelapisJurang[5].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[5].GetPooledObject();
                break;

            case "Jurang6":
                pelapis = thePelapisJurang[6].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[6].GetPooledObject();
                break;


            case "Jurang7":
                pelapis = thePelapisJurang[7].GetPooledObject();
                pelapisLanjutan = thePelapisLanjutanJurang[7].GetPooledObject();
                break;

            default:
                pelapis = thePelapisJurang[0].GetPooledObject();

                pelapisLanjutan = thePelapisLanjutanJurang[7].GetPooledObject();
                break;
        }


        pelapis.transform.position = new Vector3(transform.position.x, transform.position.y-0.01F, transform.position.z - 1);
        pelapis.transform.rotation = transform.rotation;
        pelapis.SetActive(true);

        pelapisLanjutan.transform.position = new Vector3(transform.position.x, transform.position.y - 15.8f, transform.position.z);
        pelapisLanjutan.transform.rotation = transform.rotation;
        pelapisLanjutan.SetActive(true);
    }

    private void UsePelapisPalingDepan(GameObject Field)
    {
        GameObject PelapisTerdepan;



        switch (Field.tag)
        {
            case "Field":
                PelapisTerdepan = thePelapisTerdepan[0].GetPooledObject();
                break;

            case "Field1":
                PelapisTerdepan = thePelapisTerdepan[1].GetPooledObject();
                break;
            case "Field2":
                PelapisTerdepan = thePelapisTerdepan[2].GetPooledObject();
                break;
            case "Field3":
                PelapisTerdepan = thePelapisTerdepan[3].GetPooledObject();
                break;
            case "Field4":
                PelapisTerdepan = thePelapisTerdepan[4].GetPooledObject();
                break;
            case "Field5":
                PelapisTerdepan = thePelapisTerdepan[5].GetPooledObject();
                break;
            case "Field6":
                PelapisTerdepan = thePelapisTerdepan[6].GetPooledObject();
                break;
            case "Field7":
                PelapisTerdepan = thePelapisTerdepan[7].GetPooledObject();
                break;
            case "Field8":
                PelapisTerdepan = thePelapisTerdepan[8].GetPooledObject();
                break;
            case "Field9":
                PelapisTerdepan = thePelapisTerdepan[9].GetPooledObject();
                break;
            case "Field10":
                PelapisTerdepan = thePelapisTerdepan[10].GetPooledObject();
                break;
            case "Field11":
                PelapisTerdepan = thePelapisTerdepan[11].GetPooledObject();
                break;
            case "Field12":
                PelapisTerdepan = thePelapisTerdepan[12].GetPooledObject();
                break;
            case "Field13":
                PelapisTerdepan = thePelapisTerdepan[13].GetPooledObject();
                break;
            case "Field14":
                PelapisTerdepan = thePelapisTerdepan[14].GetPooledObject();
                break;
            case "Field15":
                PelapisTerdepan = thePelapisTerdepan[15].GetPooledObject();
                break;
            default:
                PelapisTerdepan = thePelapisTerdepan[0].GetPooledObject();
                break;
        }

        PelapisTerdepan.transform.position = new Vector3(transform.position.x, transform.position.y-4, transform.position.z);
        PelapisTerdepan.transform.rotation = transform.rotation;
        PelapisTerdepan.SetActive(true);

    }

    private void SlowMoAppear()
    {
        if (Random.Range(0, 100) < PowerUpThreshold)
        {
            //menentukan Posisi X Obstacle, secara random
            GameObject Obstacle = thePowerUpPooler[4].GetPooledObject();
            //Obstacle.transform.position = new Vector2 (Random.Range(transform.position.x + 8, transform.position.x), transform.position.y + 6);
            //Menentukan X,Y berdasarkan awal dari setiap platform (Disambungan)
            Obstacle.transform.position = new Vector2(transform.position.x - 8, transform.position.y + 7f);
            Obstacle.transform.rotation = transform.rotation;
            Obstacle.SetActive(true);
        }
    }

    public void setPlatformGenerator(int landmarkAkanMuncul)
    {
        switch (landmarkAkanMuncul)
        {
            case 0:  //Sebelum Bogor
                ThresholdJurangHappen = 10;
                jurangObsThreshold = 20;
                penandaJurangCoinThreshold = 10;
                jurangCoinThreshold = 20;
                fieldCoinThreshold = 20;
                fieldObsThreshold = 50;
                PowerUpThreshold = 40;
                BounceThreshold = 10;

                break;

            case 1:  //Sebelum Jakarta
                ThresholdJurangHappen = 20;
                jurangObsThreshold = 20;
                penandaJurangCoinThreshold = 30;
                jurangCoinThreshold = 30;
                fieldCoinThreshold = 40;
                fieldObsThreshold = 40;
                PowerUpThreshold = 20;
                BounceThreshold = 15;
                break;

            case 2:  //Sebelum Bandung
                ThresholdJurangHappen = 30;
                jurangObsThreshold = 30;
                penandaJurangCoinThreshold = 40;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 50;
                fieldObsThreshold = 30;
                PowerUpThreshold = 50;
                BounceThreshold = 18;

                break;

            case 3:  //Sebelum Borobudur
                ThresholdJurangHappen = 30;
                jurangObsThreshold = 40;
                penandaJurangCoinThreshold = 50;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 30;
                fieldObsThreshold = 40;
                PowerUpThreshold = 20;
                BounceThreshold = 20;

                break;

            case 4:  //Sebelum sURABAYA
                ThresholdJurangHappen = 40;
                jurangObsThreshold = 50;
                penandaJurangCoinThreshold = 55;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 40;
                fieldObsThreshold = 50;
                PowerUpThreshold = 60;
                BounceThreshold = 20;

                break;

            case 5:  //Sebelum Bali
                ThresholdJurangHappen = 55;
                jurangObsThreshold = 70;
                penandaJurangCoinThreshold = 30;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 40;
                fieldObsThreshold = 60;
                PowerUpThreshold = 40;
                BounceThreshold = 20;

                break;

            case 6:  //Sebelum Papun
                ThresholdJurangHappen = 60;
                jurangObsThreshold = 75;
                penandaJurangCoinThreshold = 70;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 55;
                fieldObsThreshold = 60;
                PowerUpThreshold = 40;
                BounceThreshold = 30;


                break;

            case 7:  //Sebelum Aceh
                ThresholdJurangHappen = 70;
                jurangObsThreshold = 80;
                penandaJurangCoinThreshold = 75;
                jurangCoinThreshold = 40;
                fieldCoinThreshold = 40;
                fieldObsThreshold = 75;
                PowerUpThreshold = 50;
                BounceThreshold = 30;
                break;
        }

    }
}










