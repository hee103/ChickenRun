<img src="https://readme-decorate.vercel.app/api/get?type=star&text=Chicken%20Run&width=1200&height=200&fontSize=80&fontWeight=800&useGradient=true&fontColor=%23FFFFFF&backgroundColor=%23c9c9c9&gradientColor1=%23FFFFFF&gradientColor2=%230000FF">

# Description
## 개발 기간
- 2025.08 ~
- 개발 인원: 1인

## 프로젝트 소개
닭들이 농장에서 탈출 했습니다!
농부는 모든 닭을 잡아 농장에 돌려 보내야 합니다.
1명의 농부 플레이어와 4명의 닭 플레이어로 이루어진 멀티 게임입니다.

---

<details>
  <summary>조작키</summary>

- 이동 : WASD  
- 공격 / 줍기 : 마우스 좌클릭  
- 점프 : Space  
- 회전 : 마우스  

</details>

---

# 🎲 주요 기능

<details>
  <summary>멀티 시스템</summary>

### 멀티 시스템
1. 네트워크 연결 및 로비 관리

- Photon 서버에 접속 후 자동으로 씬 동기화 (PhotonNetwork.AutomaticallySyncScene = true)
- 역할 선택 버튼 클릭 시 JoinRandomRoom()으로 랜덤 방 참가 시도
- 참가 가능한 방이 없을 경우 자동으로 새 방 생성 (RoomOptions.MaxPlayers = 5)
- PlayerProperties를 활용해 각 클라이언트의 역할(Farmer / Chicken) 저장

2. 플레이어 역할 동기화 및 씬 전환

- 플레이어 속성 변경(OnPlayerPropertiesUpdate) 시, 방 내 Farmer/Chicken 인원 집계
- 조건 충족 시(1 Farmer, 4 Chicken) 방장이 PhotonNetwork.LoadLevel("Main")으로 씬 전환
- 모든 클라이언트가 동일한 씬을 로드하도록 구현

3. 역할 기반 캐릭터 스폰

- GameManager에서 PlayerProperties를 참고하여 내 역할에 맞는 캐릭터 프리팹 선택
- Chicken 캐릭터는 PlayerList 순서에 따라 고유 프리팹 적용
- PhotonNetwork.Instantiate로 네트워크 동기화된 캐릭터 생성
- SpawnPoint 배열을 통해 역할별 시작 위치 지정


</details>

---

### 트러블 슈팅
- 카메라 회전문제 발생
  해결 과정: [블로그 바로가기](https://unihee1.tistory.com/110)



