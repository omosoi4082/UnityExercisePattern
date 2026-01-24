## 설명

이 프로젝트는 Unity 6 환경에서 여러 아키텍처와 디자인 패턴을 하나의 프로젝트 안에서 연습하기 위해 만든 학습용 프로젝트입니다.

프로젝트의 목적은 단순히 기능을 구현하는 것이 아니라,
패턴을 분리해서 이해하고, 실제 시스템에서 어떻게 사용되는지를 구조적으로 연습하는 것입니다.

## [Core 영역]

Assets/\_Core 폴더는 프로젝트 전반에서 공통으로 사용되는 기반 코드 영역입니다.
이 영역의 코드는 게임플레이 로직에 의존하지 않도록 설계되었습니다.

Async

UniTask 기반 비동기 처리 보조 코드

CancellationToken 관리

Addressables

Addressables 초기화

서버(Remote) 콘텐츠 로딩

다운로드 관리 로직

Events

Observer 패턴 기반 이벤트 구조

시스템 간 결합도 감소 목적

Utils

공통 유틸리티 및 헬퍼 클래스

## [패턴 연습 영역]

Assets/Patterns 폴더는 디자인 패턴을 개별적으로 연습하기 위한 공간입니다.
각 패턴은 서로 영향을 주지 않도록 분리되어 있습니다.

연습하는 패턴:

Singleton

Observer

Command

State

Strategy

Factory

Object Pool

MVC / MVP / MVVM

이 영역의 목적은
“어떻게 구현하는가”보다
“언제, 왜 사용하는가”를 이해하는 것입니다.

## [시스템 영역]

Assets/Systems 폴더는 실제 실무에서 사용될 수 있는 시스템 단위 구현 영역입니다.
Patterns 영역에서 연습한 패턴들이 이곳에서 실제로 사용됩니다.

Loading

로딩 흐름과 비동기 제어

SaveLoad

데이터 저장 및 불러오기 전략

Network(Mock)

실제 네트워크 없이 구조만 검증하기 위한 모의 네트워크

Patch

Addressables 카탈로그 확인

서버 콘텐츠 업데이트 흐름 연습

UI

UI 상태 관리 및 구조 분리

## [게임플레이 영역]

Assets/Gameplay 폴더는 실제 게임 로직이 들어가는 영역입니다.

Player 관련 로직

Enemy 행동 로직

상호작용 처리

이 영역은 Systems와 Core를 사용하지만, 직접 의존하지 않도록 설계하는 것을 목표로 합니다.

## [실험 영역]

Assets/Experiments 폴더는 구조에 얽매이지 않고 자유롭게 실험하는 공간입니다.
실패해도 상관없는 테스트 코드가 들어갑니다.

## [씬 구성]

Boot 씬

프로젝트 시작 지점

Addressables 초기화

공통 서비스 등록

PatternLab 씬

각 디자인 패턴을 직접 실행하고 비교하는 테스트 씬

AddressablesLab 씬

서버에서 Addressables 콘텐츠를 다운로드하는 실습용 씬

## [테스트]

Assets/Tests 폴더는
유닛 테스트 및 플레이 모드 테스트를 위한 공간입니다.

## [설계 의도 요약]

이 프로젝트는

디자인 패턴을 분리해서 학습하고

실무 시스템과 연결해 보고

Unity 6 기반 아키텍처 감각을 기르기 위한 구조입니다.

단순 연습용이 아니라,
추후 포트폴리오 또는 아키텍처 참고 자료로 활용하는 것을 목표로 합니다.
